using AchievementService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.Controllers
{
    [ApiController]
    [Route("[controller]/{playerId}")]
    public class AchievementController : ControllerBase
    {
        private readonly AchievementDbContext _achievementDbContext;

        public AchievementController(AchievementDbContext achievementDbContext)
        {
            _achievementDbContext = achievementDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAsync(Guid playerId)
        {
            return await _achievementDbContext.Achievements.Where(achievement => achievement.PlayerId == playerId).ToListAsync();
        }
    }
}
