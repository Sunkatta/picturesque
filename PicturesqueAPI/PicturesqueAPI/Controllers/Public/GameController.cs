using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picturesque.DB;
using Picturesque.Domain;
using Picturesque.Services;

namespace PicturesqueAPI.Controllers.Public
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GameController : Controller
    {
        private readonly IGameServiceManager _gameManager;

        public GameController(PicturesqueDbContext ctx)
        {
            _gameManager = new GameServiceManager(ctx);
        }

        [HttpGet("GetGameOptions")]
        public async Task<IActionResult> GetGameOptions()
        {
            try
            {
                return Ok(await _gameManager.GetGameOptions());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}