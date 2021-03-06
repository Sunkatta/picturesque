﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picturesque.Application;
using Picturesque.Domain;

namespace PicturesqueAPI.Controllers.Public
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GameController : Controller
    {
        private readonly IGameServiceManager _gameManager;

        public GameController(IGameServiceManager gameManager)
        {
            _gameManager = gameManager;
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

        [HttpPost("StartGame")]
        public async Task<IActionResult> StartGame([FromBody] GameOptionsEntry entry)
        {
            try
            {
                return Ok(await _gameManager.CreateGame(entry));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}