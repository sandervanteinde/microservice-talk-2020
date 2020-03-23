using Microsoft.EntityFrameworkCore;
using PlayerService.Database;
using PlayerService.Messages;
using Shared.Messaging;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class QuestCompletedMessageHandler : IMessageHandler<QuestCompleted>
    {
        private readonly PlayerDbContext _context;
        private readonly IMessagePublisher _messagePublisher;

        public QuestCompletedMessageHandler(PlayerDbContext context, IMessagePublisher messagePublisher)
        {
            _context = context;
            _messagePublisher = messagePublisher;
        }
        public async Task HandleMessageAsync(string messageType, QuestCompleted obj)
        {
            var player = await _context.Players.FirstOrDefaultAsync(player => player.Id == obj.PlayerId);
            if (player == null)
            {
                return;
            }

            player.Experience += obj.Quest.ExperienceReward;
            player.Gold += obj.Quest.GoldReward;
            var previousLevel = player.Level;
            var newLevel = player.Experience / 100 + 1;
            if(previousLevel != newLevel)
            {
                player.Level = newLevel;
                await _messagePublisher.PublishMessageAsync("PlayerLevelled", new { obj.PlayerId, NewLevel = newLevel });
            }
            await _context.SaveChangesAsync();
        }
    }
}
