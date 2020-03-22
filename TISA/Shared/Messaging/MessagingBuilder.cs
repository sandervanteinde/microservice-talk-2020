using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public class MessagingBuilder
    {
        private readonly IServiceCollection _services;
        private Dictionary<string, Type> _messageHandlers = new Dictionary<string, Type>();
        internal IReadOnlyDictionary<string, Type> MessageHandlers => new ReadOnlyDictionary<string, Type>(_messageHandlers);
        internal MessagingBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public MessagingBuilder WithHandler<T>(string messageType)
            where T : IMessageHandler
        {
            var type = typeof(T);
            _services.AddScoped(type);
            _messageHandlers.Add(messageType, type);
            return this;
        }
    }
}
