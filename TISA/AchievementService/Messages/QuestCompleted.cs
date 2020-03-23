using System;

namespace AchievementService.Messages
{
    public class QuestCompleted : IPlayerIdMessage
    {
        public Guid PlayerId { get; set; }
        public Quest Quest { get; set; }
    }
}