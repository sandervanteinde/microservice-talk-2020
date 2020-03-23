using System;
using System.Collections.Generic;

namespace Shared.Messaging
{
    /// <summary>
    /// Registry services of all message handlers in the current project.
    /// </summary>
    internal class MessageHandlerRepository
    {
        private readonly IReadOnlyDictionary<string, Type> _messageHandlers;

        internal MessageHandlerRepository(IReadOnlyDictionary<string, Type> messageHandlers)
        {
            _messageHandlers = messageHandlers;
        }

        public bool TryGetHandlerForMessageType(string messageType, out Type type)
        {
            return _messageHandlers.TryGetValue(messageType, out type);
        }
    }
}
