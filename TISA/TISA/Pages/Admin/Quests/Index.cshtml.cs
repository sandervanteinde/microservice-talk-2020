using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Admin.Quests
{
    public class IndexModel : PageModel
    {
        private readonly IQuestService _questService;

        public IEnumerable<Quest> Quests { get; private set; }

        [BindProperty]
        public DeleteForm Form { get; set; }

        public IndexModel(IQuestService questService)
        {
            _questService = questService;
        }


        public async Task OnGetAsync()
        {
            Quests = await _questService.GetAllQuestsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _questService.DeletByIdAsync(Form.QuestId.Value);
            }

            return RedirectToPage();
        }

        public class DeleteForm
        {
            [Required]
            public Guid? QuestId { get; set; }
        }
    }
}