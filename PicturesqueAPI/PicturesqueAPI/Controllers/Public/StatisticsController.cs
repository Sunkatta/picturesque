using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picturesque.Application;
using Picturesque.Domain;

namespace PicturesqueAPI.Controllers.Public
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsServiceManager _statisticsServiceManager;

        public StatisticsController(IStatisticsServiceManager statisticsServiceManager)
        {
            _statisticsServiceManager = statisticsServiceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _statisticsServiceManager.GetTop20PlayersAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateGameScore")]
        public async Task<IActionResult> CreateGameScore([FromBody]CreateGameScoreEntry entry)
        {
            try
            {
                GameScore gc = new GameScore(
                    entry.UserId,
                    entry.CategoryId,
                    entry.Score,
                    entry.CompletedInSeconds,
                    entry.NumberOfMistakes,
                    (Difficulty)entry.Difficulty,
                    entry.IsHelpUsed);

                await _statisticsServiceManager.CreateGameScoreAsync(gc);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}