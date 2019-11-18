using Microsoft.EntityFrameworkCore;
using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
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

        public async Task GetGameOptions()
        {
            var categories = await _ctx.Categories.ToListAsync();
            var difficulties = Enum.GetValues(typeof(Difficulty));
        }
    }
}
