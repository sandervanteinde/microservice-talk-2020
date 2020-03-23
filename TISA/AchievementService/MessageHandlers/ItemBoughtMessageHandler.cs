using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;

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
