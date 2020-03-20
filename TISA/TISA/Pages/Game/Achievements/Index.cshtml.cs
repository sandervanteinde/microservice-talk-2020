using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TISA.Models;
using TISA.Services;

namespace TISA.Pages.Game.Achievements
{
    public class IndexModel : PageModel
    {
        private readonly IAchievementService _achievementService;

        public ICollection<Achievement> Achievements { get; set; }
        public IndexModel(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }
        public async Task OnGetAsync()
        {
            Achievements = await _achievementService.GetAchievementsForPlayerAsync();
        }
    }
}