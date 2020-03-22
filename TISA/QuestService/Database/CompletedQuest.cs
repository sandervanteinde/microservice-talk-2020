using System;

namespace QuestService.Database
{
    public class CompletedQuest
    {
        public Guid QuestId { get; set; }
        public Guid PlayerId { get; set; }
    }
}
