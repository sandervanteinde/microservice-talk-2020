using System;

namespace AchievementService.Messages
{
    public class ItemBought : IPlayerIdMessage
    {
        public Item Item { get; set; }
        public Guid PlayerId { get; set; }
    }
}