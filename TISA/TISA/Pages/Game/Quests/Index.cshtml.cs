using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Game.Quests
{
    public class IndexModel : PageModel
    {
        private readonly IQuestService _questService;

        public List<Quest> Quests { get; set; }

        public IndexModel(IQuestService questService)
        {
            _questService = questService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Quests = (await _questService.GetAvailableQuestsForPlayerAsync()).ToList();
            if(Quests.Count == 0)
            {
                return RedirectToPage("Finish");
            }

            return Page();
        }
    }
}