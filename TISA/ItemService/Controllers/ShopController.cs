using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly ItemDbContext _itemDbContext;

        public ShopController(ItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAsync()
        {
            return await _itemDbContext.Items.Where(item => item.AvailableAtShop).ToListAsync();
        }
    }
}
