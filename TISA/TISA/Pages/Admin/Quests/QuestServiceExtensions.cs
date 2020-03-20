using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Admin.Quests
{
    public static class QuestServiceExtensions
    {
        public static async Task<ICollection<SelectListItem>> GetSelectListItemsForComesAfterQuestIdAsync(this IQuestService questService, Guid? questId = null)
        {
            var allQuestsAsDictionary = (await questService.GetAllQuestsAsync()).ToDictionary(quest => quest.Id, quest => quest);
            var emptyEntry = new[] { new SelectListItem { Selected = true, Text = "(players start with this quest)", Value = string.Empty } };

            IEnumerable<Quest> quests;
            if(questId.HasValue)
            {
                quests = allQuestsAsDictionary
                    .Where(quest => quest.Key != questId && IsQuestIdCircularReferenceFrom(quest.Value))
                    .Select(quest => quest.Value);
            }
            else
            {
                quests = allQuestsAsDictionary.Values;
            }

            var selectListQuests = quests.Select(quest => new SelectListItem { Value = quest.Id.ToString(), Text = quest.Name });

            return emptyEntry.Concat(selectListQuests).ToList();

            bool IsQuestIdCircularReferenceFrom(Quest quest)
            {
                if(quest.ComesAfterQuestId == null)
                {
                    return true;
                }
                if(quest.ComesAfterQuestId == questId)
                {
                    return false;
                }

                return IsQuestIdCircularReferenceFrom(allQuestsAsDictionary[quest.ComesAfterQuestId.Value]);
            }
        }

    }
}
