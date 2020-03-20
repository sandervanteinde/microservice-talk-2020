using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class QuestService : IQuestService
    {
        static QuestService()
        {
            var quest1 = new Quest { Id = Guid.NewGuid(), Name = "Introductionary Quest", Description = "This is an easy introductionary quest", ExperienceReward = 100, GoldReward = 100 };
            var quest2 = new Quest { Id = Guid.NewGuid(), Name = "Second quest", Description = "The quests are getting harder now, complete this very difficult quest", ExperienceReward = 200, GoldReward = 200, ComesAfterQuestId = quest1.Id };
            _quests = new List<Quest> { quest1, quest2 };
        }

        private static readonly List<Quest> _quests;
        private static readonly Dictionary<Player, HashSet<Guid>> _completedQuests = new Dictionary<Player, HashSet<Guid>>();
        private readonly IPlayerService _playerService;

        public QuestService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task CompleteQuestAsync(Guid questId)
        {
            var completedQuests = GetCompletedQuestsForPlayer();
            completedQuests.Add(questId);

            // player management happening in quest service, bad practice!
            var player = _playerService.Player;
            var quest = await GetQuestByIdAsync(questId);
            player.Experience += quest.ExperienceReward;
            player.Level = (player.Experience / 100) + 1;
            player.Gold += quest.GoldReward;
        }

        public Task CreateAsync(Quest quest)
        {
            quest.Id = Guid.NewGuid();
            _quests.Add(quest);
            return Task.CompletedTask;
        }

        public Task DeletByIdAsync(Guid questId)
        {
            var index = _quests.FindIndex(c => c.Id == questId);
            if (index >= 0)
            {
                _quests.RemoveAt(index);
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Quest>> GetAllQuestsAsync()
        {
            return Task.FromResult<IEnumerable<Quest>>(_quests);
        }

        public Task<IEnumerable<Quest>> GetAvailableQuestsForPlayerAsync()
        {
            var completedQuests = GetCompletedQuestsForPlayer();
            var incompleteQuests = _quests.Where(quest => !completedQuests.Contains(quest.Id));

            return Task.FromResult(incompleteQuests.Where(IsQuestAvailable));

            bool IsQuestAvailable(Quest quest)
            {
                // It is not coming after a quest id, thus the player starts with it. Or the player completed the prequest
                return !quest.ComesAfterQuestId.HasValue || completedQuests.Contains(quest.ComesAfterQuestId.Value);
            }
        }

        public Task<Quest> GetQuestByIdAsync(Guid questId)
        {
            return Task.FromResult(_quests.FirstOrDefault(quest => quest.Id == questId));
        }

        public async Task UpdateQuestByIdAsync(Guid questId, Quest newQuest)
        {
            var quest = await GetQuestByIdAsync(questId);
            _quests.Remove(quest);
            newQuest.Id = questId;
            _quests.Add(newQuest);
        }

        private HashSet<Guid> GetCompletedQuestsForPlayer()
        {
            if (!_playerService.IsPlayerDefined)
            {
                throw new InvalidOperationException("Can't retrieve completed quests for player without a player being defined in the player service");
            }

            var player = _playerService.Player;

            if (!_completedQuests.TryGetValue(player, out var completedQuests))
            {
                _completedQuests.Add(player, completedQuests = new HashSet<Guid>());
            }

            return completedQuests;
        }
    }
}
