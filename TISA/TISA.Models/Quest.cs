using System;

namespace TISA.Models
{
    public class Quest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GoldReward { get; set; }
        public int ExperienceReward { get; set; }
        public Guid? ComesAfterQuestId { get; set; }
    }
}
