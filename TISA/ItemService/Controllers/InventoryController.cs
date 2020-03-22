using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;

namespace ItemService.Controllers
{
    [ApiController]
    [Route("[controller]/{playerId}")]
    public class InventoryController : Controller
    {
        private readonly ItemDbContext _dbContext;
        private readonly IMessagePublisher _messagePublisher;

        public InventoryController(ItemDbContext dbContext, IMessagePublisher messagePublisher)
        {
            _dbContext = dbContext;
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAsync(Guid playerId)
        {
            return await _dbContext.PlayerItems
                .Include(item => item.Item)
                .Where(item => item.PlayerId == playerId)
                .Select(item => item.Item)
                .ToArrayAsync();
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyAsync(Guid playerId, [FromBody] Guid itemId)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(item => item.Id == itemId);
            if(item == null)
            {
                return BadRequest();
            }
            _dbContext.PlayerItems.Add(new PlayerItem
            {
                ItemId = itemId,
                PlayerId = playerId
            });
            await _dbContext.SaveChangesAsync();

            await _messagePublisher.PublishMessageAsync("ItemBought", new { Item = item, PlayerId = playerId });

            return Ok();
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellAsync(Guid playerId, [FromBody] Guid itemId)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(item => item.Id == itemId);
            if (item == null)
            {
                return BadRequest();
            }
            var entry = await _dbContext.PlayerItems.FirstOrDefaultAsync(entry => entry.ItemId == itemId && entry.PlayerId == playerId);
            if(entry != null)
            {
                _dbContext.PlayerItems.Remove(entry);
                await _dbContext.SaveChangesAsync();
                await _messagePublisher.PublishMessageAsync("ItemSold", new { Item = item, PlayerId = playerId });
            }

            return Ok();
        }
    }
}
