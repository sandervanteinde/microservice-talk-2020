using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.Database
{
    public class AchievementDbContext : DbContext
    {

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<PlayerInformation> PlayerStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlayerInformation>()
                .Property(c => c.Id)
                .ValueGeneratedNever();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,7000;Database=AchievementService;User Id=sa;Password=HelloStudents!");
        }
    }
}
