using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class UserServiceManager : IUserServiceManager
    {
        private readonly PicturesqueDbContext _ctx;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailService;

        public UserServiceManager(
            PicturesqueDbContext ctx,
            IConfiguration config,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailService
        )
        {
            _ctx = ctx;
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<bool> BlockAsync(string id)
        {
            User user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
            user.IsBlocked = !user.IsBlocked;
            _ctx.Update(user);
            await _ctx.SaveChangesAsync();

            return user.IsBlocked;
        }

        public async Task<bool> CheckIfUserExistsByEmail(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> CheckIfUserExistsByUsername(string username)
        {
            User user = await _userManager.FindByNameAsync(username);

            return user != null;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            User user = await _userManager.FindByEmailAsync(email);
            string codeDecoded = Base64UrlEncoder.Decode(code);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            return result.Succeeded;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
            string code = Base64UrlEncoder.Encode(await _userManager.GenerateEmailConfirmationTokenAsync(user));
            var confirmationLink = $"http://localhost:62455/Account/ConfirmEmail?email={user.Email}&emailConfirmationKey={code}";
            await _emailService.SendEmailAsync(user.Email, "Confirm your Account", $"Confirm your email <a href='{confirmationLink}' target='_blanc'>HERE</a>, boyo");
        }

        public async Task<string> GenerateJWTAsync(LoginUserEntry login)
        {
            User rawUser = await _userManager.FindByEmailAsync(login.Email);
            string tokenString = "";

            if (rawUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(rawUser, login.Password, false, false);

                if (result.Succeeded)
                {
                    LoginUserEntry user = new LoginUserEntry
                    {
                        Id = rawUser.Id,
                        Email = rawUser.Email,
                        IsAdmin = rawUser.IsAdmin,
                        Username = rawUser.UserName,
                    };

                    tokenString = GenerateJSONWebToken(user);
                }
            }

            return tokenString;
        }

        public async Task<IEnumerable<UserView>> GetAll()
        {
            List<User> rawUsers = await _ctx.Users.ToListAsync();
            List<UserView> users = _mapper.Map<List<UserView>>(rawUsers);

            return users;
        }

        public async Task<User> GetRawUserByEmailAsync(string email)
        {
            User user =
                    await _ctx.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == email);

            return user != null ? user : null;
        }

        public async Task<User> GetRawUserByIdAsync(string id)
        {
            User user =
                await _ctx.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return user != null ? user : null;
        }

        public async Task<bool> IsBlocked(string email)
        {
            User user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user != null ? user.IsBlocked : false;
        }

        public async Task<bool> IsEmailConfiemd(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            return user != null ? await _userManager.IsEmailConfirmedAsync(user) : false;
        }

        private string GenerateJSONWebToken(LoginUserEntry userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Username),
                new Claim("is_admin", userInfo.IsAdmin.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
