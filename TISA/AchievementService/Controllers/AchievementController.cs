using AchievementService.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AchievementService.Controllers
{
    [ApiController]
    [Route("[controller]/{playerId}")]
    public class AchievementController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Achievement>> Get(Guid playerId)
        {
            return Ok(Enumerable.Empty<Achievement>());
        }
    }
}
