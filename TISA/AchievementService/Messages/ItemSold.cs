using System;

namespace AchievementService.Messages
{
    public class ItemSold : IPlayerIdMessage
    {
        public Item Item { get; set; }
        public Guid PlayerId { get; set; }
    }
}