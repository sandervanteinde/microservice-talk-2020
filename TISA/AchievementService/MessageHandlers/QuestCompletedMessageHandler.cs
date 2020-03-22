using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;

namespace AchievementService.MessageHandlers
{
    internal class QuestCompletedMessageHandler : PlayerSettingsHandlerBase<QuestCompleted>
    {
        public QuestCompletedMessageHandler(AchievementDbContext dbContext, DetermineAchievementsService achievementService) : base(dbContext, achievementService)
        {
        }

        protected override void ModifyPlayerStatistics(PlayerInformation playerStatistics, QuestCompleted message)
        {
            playerStatistics.CompletedQuests++;
        }
    }
}
