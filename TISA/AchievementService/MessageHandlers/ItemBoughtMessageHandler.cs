using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.MessageHandlers
{
    internal class ItemBoughtMessageHandler : PlayerSettingsHandlerBase<ItemBought>
    {
        public ItemBoughtMessageHandler(AchievementDbContext dbContext, DetermineAchievementsService achievementService) : base(dbContext, achievementService)
        {
        }

        protected override void ModifyPlayerStatistics(PlayerInformation playerStatistics, ItemBought message)
        {
            playerStatistics.MoneySpentInShops += message.Item.BuyPrice;
        }
    }
}
