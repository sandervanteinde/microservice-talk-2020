using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Admin.Quests
{
    public class EditModel : PageModel
    {
        private readonly IQuestService _questService;

        [BindProperty]
        public QuestForm Form { get; set; }

        public ICollection<SelectListItem> AvailableFollowupQuests { get; set; }

        public bool CanSelectAvailableQuest => AvailableFollowupQuests.Count > 1;

        public EditModel(IQuestService questService)
        {
            _questService = questService;
        }

        public Task<IActionResult> OnGetAsync(Guid questId)
        {
            return ConstructModel(questId);
        }

        public async Task<IActionResult> OnPostAsync(Guid questId)
        {
            if (!ModelState.IsValid)
            {
                return await ConstructModel(questId);
            }

            await _questService.UpdateQuestByIdAsync(questId, Form.ToQuest());

            return RedirectToPage("Index");
        }

        private async Task<IActionResult> ConstructModel(Guid questId)
        {
            AvailableFollowupQuests = await _questService.GetSelectListItemsForComesAfterQuestIdAsync(questId);

            if (Form == null)
            {

                var quest = await _questService.GetQuestByIdAsync(questId);
                if (quest == null)
                {
                    return NotFound();
                }

                Form = QuestForm.FromQuest(quest);
            }

            return Page();
        }
    }
}