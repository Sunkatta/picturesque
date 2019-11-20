using Picturesque.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Domain
{
    public interface IGameServiceManager
    {
        Task<Game> CreateGame(GameOptionsEntry entry);
        Task<GameOptions> GetGameOptions();
    }
}
