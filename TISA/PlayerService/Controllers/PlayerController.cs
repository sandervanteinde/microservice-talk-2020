using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlayerService.Database;

namespace PlayerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerDbContext _context;

        public PlayerController(PlayerDbContext context)
        {
            _context = context;
        }

        [HttpGet("{playerId}")]
        public async Task<ActionResult<Player>> GetAsync(Guid playerId)
        {
            var player = await _context.Players.FirstOrDefaultAsync(player => player.Id == playerId);
            if(player == null)
            {
                return NotFound();
            }

            return player;
        }
        
        [HttpPost] 
        public async Task<IActionResult> CreatePlayerAsync([FromBody] string playerName)
        {
            var newPlayer = new Player
            {
                Name = playerName,
                Experience = 0,
                Gold = 0,
                Level = 1,
            };
            _context.Players.Add(newPlayer);
            await _context.SaveChangesAsync();

            return Created($"/Player/{newPlayer.Id}", newPlayer);
        }
    }
}
