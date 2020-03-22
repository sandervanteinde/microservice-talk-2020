using Microsoft.EntityFrameworkCore;
using PlayerService.Database;
using PlayerService.Messages;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class ItemBoughtMessageHandler : IMessageHandler<ItemBought>
    {
        private readonly PlayerDbContext _dbContext;

        public ItemBoughtMessageHandler(PlayerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleMessageAsync(string messageType, ItemBought message)
        {
            var player = await _dbContext.Players.FirstOrDefaultAsync(player => player.Id == message.PlayerId);
            if(player != null)
            {
                player.Gold -= message.Item.BuyPrice;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
