
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class QuestService : IQuestService
    {
        private readonly IPlayerService _playerService;

        public QuestService(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public Task CompleteQuestAsync(Guid questId)
        {
            return $"https://localhost:7501/PlayerQuest/{_playerService.Player.Id}".PostJsonAsync(questId.ToString());
        }

        public Task CreateAsync(Quest quest)
        {
            return "https://localhost:7501/Quest".PostJsonAsync(quest);
        }

        public Task DeletByIdAsync(Guid questId)
        {
            return $"https://localhost:7501/Quest/{questId}".DeleteAsync();
        }

        public Task<IEnumerable<Quest>> GetAllQuestsAsync()
        {
            return $"https://localhost:7501/Quest/".GetJsonAsync<IEnumerable<Quest>>();
        }

        public Task<IEnumerable<Quest>> GetAvailableQuestsForPlayerAsync()
        {
            return $"https://localhost:7501/PlayerQuest/{_playerService.Player.Id}".GetJsonAsync<IEnumerable<Quest>>();
        }

        public Task<Quest> GetQuestByIdAsync(Guid questId)
        {
            return $"https://localhost:7501/Quest/{questId}".GetJsonAsync<Quest>();
        }

        public Task UpdateQuestByIdAsync(Guid questId, Quest quest)
        {
            return $"https://localhost:7501/Quest/{questId}".PutJsonAsync(quest);
        }
    }
}
