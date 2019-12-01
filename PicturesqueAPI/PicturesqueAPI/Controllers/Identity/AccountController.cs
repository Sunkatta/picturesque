using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Picturesque.Application;
using Picturesque.Domain;

namespace PicturesqueAPI.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IUserServiceManager _userManager;

        public AccountController(IUserServiceManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserEntry entry)
        {
            try
            {
                User user =
                    new User(
                        entry.Email,
                        entry.Username,
                        entry.Password,
                        false
                        );

                await _userManager.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginUserEntry login)
        {
            try
            {
                string token = await _userManager.GenerateJWTAsync(login);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}