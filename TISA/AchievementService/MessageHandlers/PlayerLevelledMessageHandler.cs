using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;

namespace AchievementService.MessageHandlers
{
    internal class PlayerLevelledMessageHandler : PlayerSettingsHandlerBase<PlayerLevelled>
    {
        public PlayerLevelledMessageHandler(AchievementDbContext dbContext, DetermineAchievementsService achievementService) : base(dbContext, achievementService)
        {
        }

        protected override void ModifyPlayerStatistics(PlayerInformation playerStatistics, PlayerLevelled message)
        {
            playerStatistics.PlayerLevel = message.NewLevel;
        }
    }
}
