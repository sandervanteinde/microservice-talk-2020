using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Services;

namespace TISA.Pages.Admin.Items
{
    public class EditModel : PageModel
    {
        private readonly IItemService _itemService;

        [BindProperty]
        public ItemForm Form { get; set; }

        public EditModel(IItemService itemService)
        {
            _itemService = itemService;
        }
        public async Task<IActionResult> OnGetAsync(Guid itemId)
        {
            var item = await _itemService.GetItemByIdAsync(itemId);
            if(item == null)
            {
                return NotFound();
            }

            Form = ItemForm.FromItem(item);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid itemId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _itemService.UpdateItemAsync(itemId, Form.ToItem());
            return RedirectToPage("Index");
        }
    }
}
