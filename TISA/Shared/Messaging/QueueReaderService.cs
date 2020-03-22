using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    internal class QueueReaderService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMqConnection _connection;
        private readonly QueueName _queueName;
        private readonly MessageHandlerRepository _messageHandlerRepository;
        private readonly ILogger<QueueReaderService> _logger;
        private IModel _channel;

        public QueueReaderService(
            IServiceProvider serviceProvider,
            RabbitMqConnection connection,
            QueueName queueName,
            MessageHandlerRepository messageHandlerRepository,
            ILogger<QueueReaderService> logger
        )
        {
            _serviceProvider = serviceProvider;
            _connection = connection;
            _queueName = queueName;
            _messageHandlerRepository = messageHandlerRepository;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _connection.CreateChannel();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (evt, evt2) =>
            {
                if (HandleMessage(evt2))
                {
                    _channel.BasicAck(evt2.DeliveryTag, false);
                }
                else
                {
                    _channel.BasicReject(evt2.DeliveryTag, true);
                }
            };
            _channel.BasicConsume(_queueName.Name, false, consumer);
            return Task.CompletedTask;
        }

        private bool HandleMessage(BasicDeliverEventArgs message)
        {
            if(!message.BasicProperties.Headers.TryGetValue("MessageType", out var objValue) || !(objValue is byte[] valueAsBytes) )
            {
                _logger.LogWarning("Received an unknown message in the queue {QueueName}. The message was discarded", _queueName.Name);
                return true;
            }

            var messageType = Encoding.UTF8.GetString(valueAsBytes);
            if(!_messageHandlerRepository.TryGetHandlerForMessageType(messageType, out var implementingHandler))
            {
                _logger.LogInformation("Message with message type {MessageType} was skipped because no handler was registered.", messageType);
                return true;
            }

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService(implementingHandler) as IMessageHandler;
                    handler.HandleMessageAsync(messageType, message.Body).GetAwaiter().GetResult();
                }
                _logger.LogInformation("Message with message type {MessageType} was successfully handled.", messageType);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Message with message type {MessageType} has encountered an unknown exception.", messageType);
                return false;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Dispose();
            _channel = null;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }
}
