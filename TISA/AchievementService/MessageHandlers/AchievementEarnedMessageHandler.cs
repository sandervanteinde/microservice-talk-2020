using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
