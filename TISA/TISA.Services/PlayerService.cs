using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class PlayerService : IPlayerService
    {
        private static List<Player> _players = new List<Player>();
        public Player Player { get; set; }

        public bool IsPlayerDefined => Player != null;

        public Task<Guid> CreatePlayerNameAsync(string playerName)
        {
            var player = new Player { Level = 1, Name = playerName, Experience = 0, Gold = 0, Id = Guid.NewGuid() };
            _players.Add(player);
            return Task.FromResult(player.Id);
        }

        public Task SetPlayerByPlayerIdAsync(Guid playerId)
        {
            Player = _players.Find(player => player.Id == playerId);
            return Task.CompletedTask;
        }
    }
}
