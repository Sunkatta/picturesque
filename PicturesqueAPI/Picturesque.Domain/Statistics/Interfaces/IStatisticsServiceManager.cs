using Picturesque.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface IStatisticsServiceManager
    {
        Task CreateGameScoreAsync(GameScore score);

        Task<IEnumerable<GameScoreView>> GetTop20PlayersAsync();

        Task CollectUserStatistics(UserStatisticsEntry userStatisticsEntry);
    }
}
