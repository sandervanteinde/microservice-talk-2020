using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Pages.Admin.Quests
{
    public class QuestForm
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Gold rewards")]
        public int GoldReward { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Experience reward")]
        public int ExperienceReward { get; set; }

        [Display(Name = "This quests comes after the defined quest")]
        public Guid? ComesAfterQuestId { get; set; }

        public Quest ToQuest() => new Quest
        {
            Name = Name,
            Description = Description,
            GoldReward = GoldReward,
            ExperienceReward = ExperienceReward,
            ComesAfterQuestId = ComesAfterQuestId
        };

        public static QuestForm FromQuest(Quest quest)
        {
            return new QuestForm
            {
                Name = quest.Name,
                Description = quest.Description,
                ExperienceReward = quest.ExperienceReward,
                GoldReward = quest.GoldReward,
                ComesAfterQuestId = quest.ComesAfterQuestId
            };
        }
    }
}
