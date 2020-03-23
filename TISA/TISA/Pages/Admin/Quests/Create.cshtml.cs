using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TISA.Services;

namespace TISA.Pages.Admin.Quests
{
    public class CreateModel : PageModel
    {
        private readonly IQuestService _questService;

        public ICollection<SelectListItem> AvailableFollowupQuests { get; set; }

        public bool CanSelectAvailableQuest => AvailableFollowupQuests.Count > 1;
        [BindProperty]
        public QuestForm Form { get; set; }

        public CreateModel(IQuestService questService)
        {
            _questService = questService;
        }

        public async Task OnGetAsync()
        {
            await ConstructModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await ConstructModel();
                return Page();
            }

            await _questService.CreateAsync(Form.ToQuest());

            return RedirectToPage("Index");
        }

        private async Task ConstructModel()
        {
            AvailableFollowupQuests = await _questService.GetSelectListItemsForComesAfterQuestIdAsync();
        }
    }
}