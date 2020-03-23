using System;

namespace AchievementService.Messages
{
    internal interface IPlayerIdMessage
    {
        Guid PlayerId { get; }
    }
}
