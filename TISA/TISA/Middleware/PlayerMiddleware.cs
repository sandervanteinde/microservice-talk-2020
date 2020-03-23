using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TISA.Services;

namespace TISA.Middleware
{
    /// <summary>
    /// This middleware scoops the player name out of every request and makes it available in the IPlayerService
    /// </summary>
    public class PlayerMiddleware : IMiddleware
    {
        private readonly IPlayerService _playerService;

        public PlayerMiddleware(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var session = context.Session;

            var playerName = session.GetString("PlayerId");

            if (!string.IsNullOrEmpty(playerName))
            {
                await _playerService.SetPlayerByPlayerIdAsync(Guid.Parse(playerName));
            }

            await next(context);
        }
    }
}
