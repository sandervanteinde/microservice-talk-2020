using AchievementService.Database;
using AchievementService.MessageHandlers;
using AchievementService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;

namespace AchievementService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSharedServices("Achievement Service");
            services.AddDbContext<AchievementDbContext>();
            services.AddScoped<DetermineAchievementsService>();
            services.AddSingleton<AchievementRepository>();
            services.AddMessagePublishing("AchievementService", builder =>
            {
                builder.WithHandler<ItemBoughtMessageHandler>("ItemBought")
                    .WithHandler<ItemSoldMessageHandler>("ItemSold")
                    .WithHandler<QuestCompletedMessageHandler>("QuestCompleted")
                    .WithHandler<AchievementEarnedMessageHandler>("AchievementEarned")
                    .WithHandler<PlayerLevelledMessageHandler>("PlayerLevelled");
            });

            using var context = new AchievementDbContext();
            // For easy of developing. Replace this with Migrations if you wish to persist data.
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSharedAppParts("Achievement Service");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
