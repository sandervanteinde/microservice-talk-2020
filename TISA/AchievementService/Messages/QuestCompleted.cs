using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.Messages
{
    public class QuestCompleted : IPlayerIdMessage
    {
        public Guid PlayerId { get; set; }
        public Quest Quest { get; set; }
    }
}