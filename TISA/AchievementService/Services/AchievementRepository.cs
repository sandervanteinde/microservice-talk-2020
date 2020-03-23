using AchievementService.Database;
using System;

namespace AchievementService.Services
{
    public class AchievementRepository
    {
        public Achievement Level2Achieved = new Achievement
        {
            Name = "Level 2 reached",
            AchievementId = Guid.NewGuid(),
            Points = 10
        };

        public Achievement Level3Achieved = new Achievement
        {
            Name = "Level 3 reached",
            AchievementId = Guid.NewGuid(),
            Points = 10
        };

        public Achievement HundredGoldSpentInShops = new Achievement
        {
            Name = "100 gold spent in shops",
            AchievementId = Guid.NewGuid(),
            Points = 10
        };

        public Achievement HundredGoldSoldInShops = new Achievement
        {
            Name = "100 gold sold in shops",
            AchievementId = Guid.NewGuid(),
            Points = 10
        };

        public Achievement TwoAchievementsEarned = new Achievement
        {
            Name = "Two achievements earned",
            AchievementId = Guid.NewGuid(),
            Points = 10
        };

        public Achievement FiveAchievementsEarned = new Achievement
        {
            Name = "Five achievements earned",
            AchievementId = Guid.NewGuid(),
            Points = 100
        };

        public Achievement TwoQuestsCompleted = new Achievement
        {
            Name = "Two quests completed",
            AchievementId = Guid.NewGuid(),
            Points = 10
        };
    }
}
