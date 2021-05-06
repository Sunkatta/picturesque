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
    public sealed class StatisticsServiceManager : IStatisticsServiceManager
    {
        private readonly PicturesqueDbContext _ctx;
        private readonly ICategoryServiceManager _categoryServiceManager;
        private readonly IUserServiceManager _userServiceManager;

        public StatisticsServiceManager(
            PicturesqueDbContext ctx,
            ICategoryServiceManager categoryServiceManager,
            IUserServiceManager userServiceManager
        )
        {
            _ctx = ctx;
            _categoryServiceManager = categoryServiceManager;
            _userServiceManager = userServiceManager;
        }

        public async Task CreateGameScoreAsync(GameScore score)
        {
            await _ctx.GameScores.AddAsync(score);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameScoreView>> GetTop20PlayersAsync()
        {
            List<GameScoreView> gameScoresViews = new List<GameScoreView>();
            var gameScores = await _ctx.GameScores.ToListAsync();

            foreach (var gc in gameScores)
            {
                var user = await _userServiceManager.GetRawUserByIdAsync(gc.UserId);
                var category = await _categoryServiceManager.GetRawCategoryById(gc.CategoryId);
                gameScoresViews.Add(
                    new GameScoreView(
                        user.UserName,
                        user.ProfilePic,
                        gc.Difficulty,
                        category.Name,
                        gc.Score,
                        gc.NumberOfMistakes,
                        gc.CompletedInSeconds,
                        gc.IsHelpUsed,
                        gc.CreatedOn)
                    );
            }

            return gameScoresViews.OrderByDescending(gc => gc.Score);
        }
    }
}
