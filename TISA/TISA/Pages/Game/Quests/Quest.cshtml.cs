using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Game.Quests
{
    public class QuestModel : PageModel
    {
        private readonly IQuestService _questService;

        public Quest Quest { get; set; }
        public QuestModel(IQuestService questService)
        {
            _questService = questService;
        }
        public async Task OnGetAsync(Guid questId)
        {
            Quest = await _questService.GetQuestByIdAsync(questId);
        }

        public async Task<IActionResult> OnPostAsync(Guid questId)
        {
            await _questService.CompleteQuestAsync(questId);
            return RedirectToPage("Index");
        }
    }
}
