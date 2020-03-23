using AchievementService.Database;
using AchievementService.Messages;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.Services
{
    /// <summary>
    /// Services for easily determining which achievements should be granted to a player
    /// </summary>
    public class DetermineAchievementsService
    {
        private readonly AchievementDbContext _dbContext;
        private readonly AchievementRepository _repository;
        private readonly IMessagePublisher _messagePublisher;

        public DetermineAchievementsService(AchievementDbContext dbContext, AchievementRepository repository, IMessagePublisher messagePublisher)
        {
            _dbContext = dbContext;
            _repository = repository;
            _messagePublisher = messagePublisher;
        }

        public async Task ProcessAchievementsForPlayerAsync(PlayerInformation information)
        {
            var achievements = DetermineAchievedAchievements(information).ToList();
            var alreadyAchieved = new HashSet<Guid>(await _dbContext.Achievements.Where(id => id.PlayerId == information.Id).Select(id => id.AchievementId).ToArrayAsync());

            // All achievements that were not achieved before, but are now achieved, are newly achieved achievements.
            var newlyAchieved = achievements.Where(achievement => !alreadyAchieved.Contains(achievement.AchievementId)).ToArray();
            if(newlyAchieved.Length == 0)
            {
                return;
            }

            // Add the new achievement to the database and publish a message
            foreach(var newAchievement in newlyAchieved)
            {
                var achievement = new Achievement
                {
                    AchievementId = newAchievement.AchievementId,
                    Name = newAchievement.Name,
                    PlayerId = information.Id,
                    Points = newAchievement.Points
                };
                _dbContext.Achievements.Add(achievement);

                await _messagePublisher.PublishMessageAsync("AchievementEarned", new AchievementEarned { PlayerId = information.Id, Achievement = achievement });
            }
        }

        /// <summary>
        /// Determine which achievements are achieved by the player
        /// </summary>
        private IEnumerable<Achievement> DetermineAchievedAchievements(PlayerInformation information)
        {
            if(information.MoneySpentInShops >= 100)
            {
                yield return _repository.HundredGoldSpentInShops;
            }

            if(information.MoneyEarnedInShops >= 100)
            {
                yield return _repository.HundredGoldSoldInShops;
            }

            if(information.CompletedQuests >= 2)
            {
                yield return _repository.TwoQuestsCompleted;
            }

            if(information.AchievementsEarned >= 2)
            {
                yield return _repository.TwoAchievementsEarned;
            }

            if(information.AchievementsEarned >= 5)
            {
                yield return _repository.FiveAchievementsEarned;
            }

            if(information.PlayerLevel >= 2)
            {
                yield return _repository.Level2Achieved;
            }

            if (information.PlayerLevel >= 3)
            {
                yield return _repository.Level3Achieved;
            }
        }
    }
}
