using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TISA.Services
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddTisaServices(this IServiceCollection services)
        {
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IQuestService, QuestService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IAchievementService, AchievementService>();
            return services;
        }
    }
}
