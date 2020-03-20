using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Game.Shop
{
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;
        private readonly IPlayerService _playerService;

        public ICollection<Item> ShopItems { get; set; }
        public ICollection<Item> PlayerInventory { get; set; }

        public int PlayerGold { get; set; }
        
        [ModelBinder]
        public PostSellForm Form { get; set; }

        public IndexModel(IItemService itemService, IPlayerService playerService)
        {
            _itemService = itemService;
            _playerService = playerService;
        }
        public async Task OnGetAsync()
        {
            PlayerInventory = await _itemService.GetItemsForPlayerAsync();
            ShopItems = await _itemService.GetShopItemsAsync();
            PlayerGold = _playerService.Player.Gold;
        }

        public async Task<IActionResult> OnPostBuyAsync()
        {
            if (ModelState.IsValid)
            {
                await _itemService.BuyPlayerItem(Form.ItemId);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSellAsync()
        {
            if (ModelState.IsValid)
            {
                await _itemService.SellPlayerItem(Form.ItemId);
            }
            return RedirectToPage();
        }

        public bool CanBuyItem(Item item)
        {
            return item.BuyPrice <= PlayerGold;
        }

        public class PostSellForm
        {
            [Required]
            public Guid ItemId { get; set; }
        }
    }
}
