using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    public interface IQuestService
    {
        Task<IEnumerable<Quest>> GetAvailableQuestsForPlayerAsync();
        Task<Quest> GetQuestByIdAsync(Guid questId);
        Task<IEnumerable<Quest>> GetAllQuestsAsync();
        Task DeletByIdAsync(Guid questId);
        Task CreateAsync(Quest quest);
        Task UpdateQuestByIdAsync(Guid questId, Quest quest);
        Task CompleteQuestAsync(Guid questId);
    }
}
