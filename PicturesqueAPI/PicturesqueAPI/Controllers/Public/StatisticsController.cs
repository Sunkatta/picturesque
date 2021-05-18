using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("UserStatistics")]
        public async Task<IActionResult> UserStatistics([FromBody] UserStatisticsEntry entry)
        {
            try
            {
                if (string.IsNullOrEmpty(entry.UserId))
                {
                    return NotFound("User not found");
                }

                await _statisticsServiceManager.CollectUserStatistics(entry);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetUserStatistics/{id}")]
        public async Task<IActionResult> GetUserStatistics(string id)
        {
            try
            {
                UserStatisticsView userStatisticsView = await _statisticsServiceManager.GetUserStatistics(id);

                return Ok(userStatisticsView);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}