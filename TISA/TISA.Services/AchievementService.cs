using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class AchievementService : IAchievementService
    {
        private readonly IPlayerService _playerService;

        public AchievementService(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public Task<ICollection<Achievement>> GetAchievementsForPlayerAsync()
        {
            return $"https://localhost:7601/Achievement/{_playerService.Player.Id}".GetJsonAsync<ICollection<Achievement>>();
        }
    }
    
}
