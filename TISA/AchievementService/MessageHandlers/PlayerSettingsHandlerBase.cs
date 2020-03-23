using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using System.Threading.Tasks;

namespace AchievementService.MessageHandlers
{
    /// <summary>
    /// Abstract class that handles the basic operations for all mutations on the PlayerInformation object
    /// </summary>
    internal abstract class PlayerSettingsHandlerBase<T> : IMessageHandler<T>
        where T : class, IPlayerIdMessage
    {
        private readonly AchievementDbContext _dbContext;
        private readonly DetermineAchievementsService _achievementService;

        public PlayerSettingsHandlerBase(AchievementDbContext dbContext, DetermineAchievementsService achievementService)
        {
            _dbContext = dbContext;
            _achievementService = achievementService;
        }

        public async Task HandleMessageAsync(string messageType, T message)
        {
            var settings = await _dbContext.PlayerStatistics.FirstOrDefaultAsync(stat => stat.Id == message.PlayerId);
            if(settings == null)
            {
                _dbContext.PlayerStatistics.Add(settings = new PlayerInformation { Id = message.PlayerId });
            }
            ModifyPlayerStatistics(settings, message);
            await _achievementService.ProcessAchievementsForPlayerAsync(settings);
            await _dbContext.SaveChangesAsync();
        }

        protected abstract void ModifyPlayerStatistics(PlayerInformation playerStatistics, T message);
    }
}
