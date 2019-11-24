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
        private static Random rand = new Random();

        private readonly PicturesqueDbContext _ctx;

        public GameServiceManager(PicturesqueDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Game> CreateGame(GameOptionsEntry gameOptions)
        {
            Category category =
                await _ctx.Categories
                .FirstOrDefaultAsync(c => c.Id == gameOptions.CategoryId);
            Difficulty difficulty;
            Enum.TryParse(gameOptions.Difficulty, out difficulty);

            int numberOfPictures = GetNumberOfPictures(difficulty);

            List<Picture> pictures = await GetPictures(category, numberOfPictures);

            return new Game(category.Id, difficulty, pictures);
        }

        public async Task<GameOptions> GetGameOptions()
        {
            var categories = await _ctx.Categories.ToListAsync();
            var difficulties = Enum.GetNames(typeof(Difficulty));

            return new GameOptions(categories, difficulties);
        }

        #region Private Methods

        private static int GetNumberOfPictures(Difficulty difficulty)
        {
            int numberOfPictures = 0;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    numberOfPictures = 8;
                    break;
                case Difficulty.Medium:
                    numberOfPictures = 18;
                    break;
                case Difficulty.Hard:
                    numberOfPictures = 32;
                    break;
                default:
                    numberOfPictures = 0;
                    break;
            }

            return numberOfPictures;
        }

        private async Task<List<Picture>> GetPictures(Category category, int numberOfPictures)
        {
            // Get the number of pictures
            var pictures =
                await _ctx.Pictures
                .Where(
                    p => p.Categories.FirstOrDefault().CategoryId == category.Id)
                .Take(numberOfPictures)
                .ToListAsync();

            // Duplicate the pictures
            pictures =
                pictures
                .SelectMany(p => Enumerable.Repeat(p, 2))
                .ToList();

            // Shuffle the list
            pictures =
                pictures
                .Select(p => new { Value = p, Order = rand.Next() })
                .OrderBy(x => x.Order)
                .Select(p => p.Value)
                .ToList();
            return pictures;
        }

        #endregion
    }
}
