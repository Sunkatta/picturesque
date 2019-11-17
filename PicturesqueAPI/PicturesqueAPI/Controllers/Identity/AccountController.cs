using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picturesque.Application;
using Picturesque.DB;
using Picturesque.Domain;
using Picturesque.Services;

namespace PicturesqueAPI.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IUserServiceManager _userManager;

        public AccountController(PicturesqueDbContext ctx)
        {
            _userManager = new UserServiceManager(ctx);
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
    }
}