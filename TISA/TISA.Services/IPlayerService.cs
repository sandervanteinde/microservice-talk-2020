using System;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    public interface IPlayerService
    {
        Player Player { get; }
        bool IsPlayerDefined => Player != null;

        Task SetPlayerByPlayerIdAsync(Guid playerId);
        Task<Guid> CreatePlayerNameAsync(string playerName);
    }
}
