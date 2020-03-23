using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;

namespace AchievementService.MessageHandlers
{
    internal class AchievementEarnedMessageHandler : PlayerSettingsHandlerBase<AchievementEarned>
    {
        public AchievementEarnedMessageHandler(AchievementDbContext dbContext, DetermineAchievementsService achievementService) : base(dbContext, achievementService)
        {
        }

        protected override void ModifyPlayerStatistics(PlayerInformation playerStatistics, AchievementEarned message)
        {
            playerStatistics.AchievementsEarned++;
        }
    }
}
