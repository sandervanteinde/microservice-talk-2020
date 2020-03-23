using AchievementService.Database;
using System;

namespace AchievementService.Messages
{
    public class AchievementEarned : IPlayerIdMessage
    {
        public Guid PlayerId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
