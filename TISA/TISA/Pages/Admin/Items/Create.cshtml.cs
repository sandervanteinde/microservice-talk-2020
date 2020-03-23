using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Services;

namespace TISA.Pages.Admin.Items
{
    public class CreateModel : PageModel
    {
        private readonly IItemService _itemService;

        [BindProperty]
        public ItemForm Form { get; set; }
        public CreateModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public void OnGet()
        {

        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _itemService.CreateItemAsync(Form.ToItem());

            return RedirectToPage("Index");
        }
    }
}