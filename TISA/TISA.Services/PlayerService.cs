using Flurl.Http;
using System;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class PlayerService : IPlayerService
    {
        public Player Player { get; private set; }

        public async Task<Guid> CreatePlayerNameAsync(string playerName)
        {
            var response = await "https://localhost:7401/Player".PostJsonAsync(playerName);
            if(response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new InvalidOperationException("Something went wrong trying to create the player");
            }

            var player = await $"https://localhost:7401{response.Headers.Location}".GetJsonAsync<Player>();
            return player.Id;
        }

        public async Task SetPlayerByPlayerIdAsync(Guid playerId)
        {
            var response = await $"https://localhost:7401/Player/{playerId}".GetJsonAsync<Player>();
            Player = response;
        }
    }
}
