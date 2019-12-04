using Picturesque.DB;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class StatisticsServiceManager : IStatisticsServiceManager
    {
        private readonly PicturesqueDbContext _ctx;

        public StatisticsServiceManager(PicturesqueDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateGameScoreAsync(GameScore score)
        {
            await _ctx.GameScores.AddAsync(score);
            await _ctx.SaveChangesAsync();
        }
    }
}
