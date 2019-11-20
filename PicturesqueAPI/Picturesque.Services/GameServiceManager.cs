using Microsoft.EntityFrameworkCore;
using Picturesque.Application;
using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class GameServiceManager : IGameServiceManager
    {
        private const int TIME_LIMIT = 10;
        private const int NUMBER_OF_MISTAKES = 5;

        private readonly PicturesqueDbContext _ctx;

        public GameServiceManager(PicturesqueDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Game> CreateGame(GameOptionsEntry gameOptions)
        {
            Category category = _ctx.Categories.FirstOrDefault(c => c.Id == gameOptions.CategoryId);
            Difficulty difficulty;
            
            Enum.TryParse(gameOptions.Difficulty, out difficulty);

            return new Game(category, difficulty);
        }

        public async Task<GameOptions> GetGameOptions()
        {
            var categories = await _ctx.Categories.ToListAsync(); ;
            var difficulties = Enum.GetNames(typeof(Difficulty));

            return new GameOptions(categories, difficulties);
        }
    }
}
