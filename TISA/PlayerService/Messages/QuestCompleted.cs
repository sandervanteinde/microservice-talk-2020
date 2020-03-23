using System;

namespace PlayerService.Messages
{
    public class QuestCompleted
    {
        public Guid PlayerId { get; set; }
        public Quest Quest { get; set; }
    }
}