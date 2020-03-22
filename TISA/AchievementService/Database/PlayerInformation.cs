using System;
using System.ComponentModel.DataAnnotations;

namespace AchievementService.Database
{
    public class PlayerInformation
    {
        [Key]
        public Guid Id { get; set; }
        public int CompletedQuests { get; set; }
        public int MoneySpentInShops { get; set; }
        public int AchievementsEarned { get; set; }
        public int MoneyEarnedInShops { get; set; }
        public int PlayerLevel { get; set; }
    }
}
