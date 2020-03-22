using AchievementService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.Messages
{
    public class AchievementEarned : IPlayerIdMessage
    {
        public Guid PlayerId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
