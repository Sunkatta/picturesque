using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Picturesque.Application;
using Picturesque.DB;
using Picturesque.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picturesque.Services
{
    public sealed class StatisticsServiceManager : IStatisticsServiceManager
    {
        private readonly PicturesqueDbContext _ctx;
        private readonly ICategoryServiceManager _categoryServiceManager;
        private readonly IUserServiceManager _userServiceManager;
        private readonly IMapper _mapper;

        public StatisticsServiceManager(
            PicturesqueDbContext ctx,
            ICategoryServiceManager categoryServiceManager,
            IUserServiceManager userServiceManager,
            IMapper mapper
        )
        {
            _ctx = ctx;
            _categoryServiceManager = categoryServiceManager;
            _userServiceManager = userServiceManager;
            _mapper = mapper;
        }

        public async Task CollectUserStatistics(UserStatisticsEntry userStatisticsEntry)
        {
            UserStatistics userStatistics = await _ctx.UserStatistics
                .FirstOrDefaultAsync(x => x.UserId == userStatisticsEntry.UserId);

            if (userStatistics != null)
            {
                userStatistics.GamesWon = userStatisticsEntry.HasWon ? ++userStatistics.GamesWon : userStatistics.GamesWon;
                userStatistics.GamesLost = userStatisticsEntry.HasWon ? userStatistics.GamesLost : ++userStatistics.GamesLost;
                userStatistics.TotalScore += userStatisticsEntry.Score;
                userStatistics.TotalNumberOfMistakes += userStatisticsEntry.NumberOfMistakes;
                userStatistics.TotalPlaytime += userStatisticsEntry.Playtime;

                _ctx.Update(userStatistics);
                await _ctx.SaveChangesAsync();
            }
            else
            {
                UserStatistics newUserStatistics = new UserStatistics(
                    userStatisticsEntry.UserId,
                    userStatisticsEntry.HasWon ? 1 : 0,
                    userStatisticsEntry.HasWon ? 0 : 1,
                    userStatisticsEntry.Score,
                    userStatisticsEntry.NumberOfMistakes,
                    userStatisticsEntry.Playtime);

                await _ctx.AddAsync(newUserStatistics);
                await _ctx.SaveChangesAsync();
            }
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

        public async Task<UserStatisticsView> GetUserStatistics(string userId)
        {
            UserStatistics userStatistics = await _ctx.UserStatistics.FirstOrDefaultAsync(x => x.UserId == userId);
            UserStatisticsView userStatisticsView = null;

            if (userStatistics != null)
            {
                userStatisticsView = _mapper.Map<UserStatisticsView>(userStatistics);
            }

            return userStatisticsView;
        }
    }
}
