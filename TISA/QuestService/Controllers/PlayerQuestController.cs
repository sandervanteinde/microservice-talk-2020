using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestService.Database;
using Shared.Messaging;

namespace QuestService.Controllers
{
    [Route("[controller]/{playerId}")]
    [ApiController]
    public class PlayerQuestController : ControllerBase
    {
        private readonly QuestDbContext _dbContext;
        private readonly IMessagePublisher _messagePublisher;

        public PlayerQuestController(QuestDbContext dbContext, IMessagePublisher messagePublisher)
        {
            _dbContext = dbContext;
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> GetActiveQuests(Guid playerId)
        {
            return await _dbContext.GetAvailableQuestsForPlayerIdAsync(playerId);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteQuest(Guid playerId, [FromBody] Guid questId) 
        {
            var quest = _dbContext.Quests.FirstOrDefault(q => q.Id == questId);
            if(quest == null)
            {
                return BadRequest();
            }

            var completeQuest = new CompletedQuest { PlayerId = playerId, QuestId = questId };
            _dbContext.CompletedQuests.Add(completeQuest);
            await _dbContext.SaveChangesAsync();


            await _messagePublisher.PublishMessageAsync("QuestCompleted", new { PlayerId = playerId, Quest = quest });

            return Ok();
        }
    }
}