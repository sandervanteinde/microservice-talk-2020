using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Game.PlayerInformation
{
    public class IndexModel : PageModel
    {
        private readonly IPlayerService _playerService;
        private readonly IItemService _itemService;

        public Player Player { get; set; }
        public ICollection<Item> PlayerInventory { get; set; }
        public bool PlayerHasItems => PlayerInventory.Count > 0;

        public IndexModel(IPlayerService playerService, IItemService itemService)
        {
            _playerService = playerService;
            _itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_playerService.IsPlayerDefined)
            {
                return NotFound();
            }

            Player = _playerService.Player;
            PlayerInventory = await _itemService.GetItemsForPlayerAsync();
            return Page();
        }
    }
}