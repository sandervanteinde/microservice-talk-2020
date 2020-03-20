using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Services;

namespace TISA.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPlayerService _playerService;

        [BindProperty]
        public IndexForm Form { get; set; }

        public IndexModel(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public IActionResult OnGet()
        {
            if (_playerService.IsPlayerDefined)
            {
                return RedirectToPage("/Game/Index");
            }

            return Page();
        }

        public async System.Threading.Tasks.Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _playerService.SetPlayerByName(Form.PlayerName);
            HttpContext.Session.SetString("PlayerName", Form.PlayerName);
            return RedirectToPage("/Game/Index");
        }

        public class IndexForm
        {
            [Required, MinLength(3)]
            public string PlayerName { get; set; }
        }
    }
}