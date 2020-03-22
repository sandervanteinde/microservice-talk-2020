using AchievementService.Database;
using AchievementService.Messages;
using AchievementService.Services;

namespace AchievementService.MessageHandlers
{
    internal class ItemSoldMessageHandler : PlayerSettingsHandlerBase<ItemSold>
    {
        public ItemSoldMessageHandler(AchievementDbContext dbContext, DetermineAchievementsService achievementService) : base(dbContext, achievementService)
        {
        }

        protected override void ModifyPlayerStatistics(PlayerInformation playerStatistics, ItemSold message)
        {
            playerStatistics.MoneyEarnedInShops = message.Item.SellPrice;
        }
    }
}
