using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Admin.Items
{
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;

        [BindProperty]
        public DeleteForm Form { get; set; }
        public ICollection<Item> Items { get; set; }

        public IndexModel(IItemService itemService)
        {
            _itemService = itemService;
        }


        public async Task OnGetAsync()
        {
            Items = await _itemService.GetAllItemsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _itemService.DeleteItemByIdAsync(Form.ItemId);
            }
            return RedirectToPage();
        }

        public class DeleteForm
        {
            [Required]
            public Guid ItemId { get; set; }
        }
    }
}