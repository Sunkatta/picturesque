using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface IStatisticsServiceManager
    {
        Task CreateGameScoreAsync(GameScore score);
        Task<IEnumerable<GameScoreView>> GetTop20PlayersAsync();
    }
}
