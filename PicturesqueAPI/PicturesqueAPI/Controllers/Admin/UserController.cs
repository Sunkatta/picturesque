using Microsoft.AspNetCore.Mvc;
using Picturesque.Domain;
using System.Threading.Tasks;

namespace PicturesqueAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private IUserServiceManager _userManager;

        public UserController(IUserServiceManager _userManager)
        {
            this._userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userManager.GetAll());
        }

        [HttpPost("Block")]
        public async Task<IActionResult> Block([FromBody] string id)
        {
            bool isBlocked = await _userManager.BlockAsync(id);

            return Ok(isBlocked);
        }
    }
}
