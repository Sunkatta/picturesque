using Microsoft.EntityFrameworkCore;
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

        public Task CreateGame()
        {
            throw new NotImplementedException();
        }

        public async Task<GameOptions> GetGameOptions()
        {
            var categories = await GetCategoriesNames();
            var difficulties = Enum.GetNames(typeof(Difficulty));

            return new GameOptions(categories, difficulties);
        }

        private async Task<List<string>> GetCategoriesNames()
        {
            List<string> categoriesNames = new List<string>();
            var categories = await _ctx.Categories.ToListAsync();

            foreach (var category in categories)
            {
                categoriesNames.Add(category.Name);
            }

            return categoriesNames;
        }
    }
}
