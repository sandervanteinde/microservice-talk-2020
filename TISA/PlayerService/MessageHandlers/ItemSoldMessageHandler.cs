using Microsoft.EntityFrameworkCore;
using PlayerService.Database;
using PlayerService.Messages;
using Shared.Messaging;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class ItemSoldMessageHandler : IMessageHandler<ItemSold>
    {
        private readonly PlayerDbContext _dbContext;

        public ItemSoldMessageHandler(PlayerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleMessageAsync(string messageType, ItemSold message)
        {
            var player = await _dbContext.Players.FirstOrDefaultAsync(player => player.Id == message.PlayerId);
            if (player != null)
            {
                player.Gold += message.Item.SellPrice;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
