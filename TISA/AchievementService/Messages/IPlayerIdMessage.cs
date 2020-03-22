using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchievementService.Messages
{
    internal interface IPlayerIdMessage
    {
        Guid PlayerId { get; }
    }
}
