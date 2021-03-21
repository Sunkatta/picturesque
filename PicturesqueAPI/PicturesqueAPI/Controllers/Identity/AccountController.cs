using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                bool isEmailAlreadyTaken = await _userManager.CheckIfUserExistsByEmail(entry.Email);

                if (isEmailAlreadyTaken)
                {
                    return BadRequest("Account with this email already exists!");
                }

                bool isUsernameAlreadyTaken = await _userManager.CheckIfUserExistsByUsername(entry.Username);

                if (isUsernameAlreadyTaken)
                {
                    return BadRequest("Account with this username already exists!");
                }

                User user = new User()
                {
                    Email = entry.Email,
                    UserName = entry.Username,
                    IsAdmin = false,
                };

                await _userManager.CreateUserAsync(user, entry.Password);
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

                bool isBlocked = await _userManager.IsBlocked(login.Email);

                if (isBlocked)
                {
                    return Forbid();
                }

                bool isEmailConfirmed = await _userManager.IsEmailConfiemd(login.Email);

                if (!isEmailConfirmed)
                {
                    return BadRequest();
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string emailConfirmationKey)
        {
            try
            {
                bool isEmailSuccessfullyConfirmed = await _userManager.ConfirmEmailAsync(email, emailConfirmationKey);
                return Ok(isEmailSuccessfullyConfirmed);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResendConfirmationEmail")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] EmailEntry entry)
        {
            try
            {
                await _userManager.ResendConfirmationEmail(entry.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailEntry entry)
        {
            try
            {
                await _userManager.ForgotPassword(entry.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordEntry entry)
        {
            try
            {
                bool isPasswordReset = await _userManager.ResetPassword(entry.Email, entry.Password, entry.PasswordResetCode);
                return Ok(isPasswordReset);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}