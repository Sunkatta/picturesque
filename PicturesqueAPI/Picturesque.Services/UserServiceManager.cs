using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Picturesque.Application;
using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class UserServiceManager : IUserServiceManager
    {
        private readonly PicturesqueDbContext _ctx;
        private readonly IConfiguration _config;

        public UserServiceManager(PicturesqueDbContext ctx, IConfiguration config)
        {
            _ctx = ctx;
            _config = config;
        }

        public async Task CreateUserAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task<string> GenerateJWTAsync(LoginUserEntry login)
        {
            LoginUserEntry user = await AuthenticateUserAsync(login);
            string tokenString = "";

            if (user != null)
            {
                tokenString = GenerateJSONWebToken(user);
            }

            return tokenString;
        }

        public async Task<User> GetRawUserByEmailAsync(string email)
        {
            User user =
                    await _ctx.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == email);

            return user != null ? user : null;
        }

        private async Task<LoginUserEntry> AuthenticateUserAsync(LoginUserEntry login)
        {
            LoginUserEntry user = null;
            User rawUser = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

            if(rawUser != null && VerifyPassword(login.Password, rawUser.Password))
            {
                user = 
                    new LoginUserEntry 
                    {
                        Id = rawUser.Id,
                        Email = rawUser.Email,
                        IsAdmin = rawUser.IsAdmin
                    };
            }
            
            return user;
        }

        private string GenerateJSONWebToken(LoginUserEntry userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("is_admin", userInfo.IsAdmin.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string plainPassword, string passwordHash)
        {
            /* Fetch the stored value & Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(passwordHash);

            /* Get the salt */
            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
