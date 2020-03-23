using System;

namespace AchievementService.Messages
{
    internal class PlayerLevelled : IPlayerIdMessage
    {
        public Guid PlayerId { get; set; }
        public int NewLevel { get; set; }
    }
}
