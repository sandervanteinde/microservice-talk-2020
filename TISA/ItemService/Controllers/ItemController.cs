using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemDbContext _itemDbContext;

        public ItemController(ItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAsync()
        {
            return await _itemDbContext.Items.ToArrayAsync();
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<Item>> GetAsync(Guid itemId)
        {
            return await _itemDbContext.Items.FirstOrDefaultAsync(item => item.Id == itemId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Item item)
        {
            _itemDbContext.Items.Add(item);
            await _itemDbContext.SaveChangesAsync();
            return Created($"Item/{item.Id}", item);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> EditAsync(Guid itemId, [FromBody] Item item)
        { 
            item.Id = itemId;
            _itemDbContext.Attach(item).State = EntityState.Modified;
            await _itemDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteAsync(Guid itemId)
        {
            _itemDbContext.Attach(new Item { Id = itemId }).State = EntityState.Deleted;
            await _itemDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
