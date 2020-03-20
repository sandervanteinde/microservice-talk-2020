using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class PlayerService : IPlayerService
    {
        private static List<Player> _players = new List<Player>();
        public Player Player { get; set; }

        public bool IsPlayerDefined => Player != null;

        public Task SetPlayerByName(string playerName)
        {
            Player = _players.Find(player => player.Name == playerName);
            if (Player == null)
            {
                Player = new Player { Level = 1, Name = playerName, Experience = 0 };
                _players.Add(Player);
            }
            return Task.CompletedTask;
        }
    }
}
