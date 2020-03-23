using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuestService.Database
{
    public class QuestDbContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; }
        public DbSet<CompletedQuest> CompletedQuests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompletedQuest>()
                .HasKey(cq => new { cq.QuestId, cq.PlayerId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,7000;Database=QuestService;User Id=sa;Password=HelloStudents!");
        }

        internal Task<Quest[]> GetAvailableQuestsForPlayerIdAsync(Guid playerId)
        {
            return Quests
                // All quests that are not completed
                .Where(q => !CompletedQuests.Where(c => c.PlayerId == playerId).Select(c => c.QuestId).Contains(q.Id))
                // All quests that does not have a prerequisite or that has its prerequisite completed
                .Where(q => q.ComesAfterQuestId == null || CompletedQuests.Where(c => c.PlayerId == playerId).Select(c => c.QuestId).Contains(q.ComesAfterQuestId.Value))
                .ToArrayAsync();
        }
    }
}
