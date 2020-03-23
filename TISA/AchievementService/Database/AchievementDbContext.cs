using Microsoft.EntityFrameworkCore;

namespace AchievementService.Database
{
    public class AchievementDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,7000;Database=AchievementService;User Id=sa;Password=HelloStudents!");
        }
    }
}
