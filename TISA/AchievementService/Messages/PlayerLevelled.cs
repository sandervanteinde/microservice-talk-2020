using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.Messages
{
    internal class PlayerLevelled : IPlayerIdMessage
    {
        public Guid PlayerId { get; set; }
        public int NewLevel { get; set; }
    }
}
