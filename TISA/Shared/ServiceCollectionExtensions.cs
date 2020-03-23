using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shared.Messaging;
using System;

namespace Shared
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSharedServices(this IServiceCollection services, string apiName)
        {
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1" }));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .AddSeq("http://localhost:7201")
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Debug);
            });
        }

        public static void AddMessagePublishing(this IServiceCollection services, string queueName, Action<MessagingBuilder> builderFn = null)
        {
            var builder = new MessagingBuilder(services);
            services.AddHostedService<QueueReaderService>();
            services.AddSingleton(new MessageHandlerRepository(builder.MessageHandlers));

            builderFn?.Invoke(builder);
            var queueNameService = new QueueName(queueName);
            services.AddSingleton(queueNameService);
            var connection = new RabbitMqConnection();
            services.AddSingleton(connection);
            services.AddScoped<IMessagePublisher, RabbitMqMessagePublisher>();
        }
    }
}
