using Microsoft.EntityFrameworkCore;

namespace PlayerService.Database
{
    public class PlayerDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,7000;Database=PlayerService;User Id=sa;Password=HelloStudents!");
        }
    }
}
