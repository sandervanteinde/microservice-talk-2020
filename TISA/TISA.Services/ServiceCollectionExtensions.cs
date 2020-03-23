using Microsoft.Extensions.DependencyInjection;

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
