using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    public interface IAchievementService
    {
        Task<ICollection<Achievement>> GetAchievementsForPlayerAsync();
    }
    
}
