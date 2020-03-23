using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class AchievementService : IAchievementService
    {
        public Task<ICollection<Achievement>> GetAchievementsForPlayerAsync()
        {
            // this is a placeholder. This part is to difficult to implement and will create a big ball of mud
            var achievement = new Achievement { Name = "Placeholder", Points = 100 };

            return Task.FromResult<ICollection<Achievement>>(new[] { achievement });
        }
    }
    
}
