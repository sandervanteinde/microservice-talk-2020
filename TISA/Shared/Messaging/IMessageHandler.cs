using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(string messageType, byte[] obj);
    }

    public interface IMessageHandler<TMessage> : IMessageHandler
        where TMessage : class
    {
        Task IMessageHandler.HandleMessageAsync(string messageType, byte[] obj)
        {
            return HandleMessageAsync(messageType, JsonSerializer.Deserialize<TMessage>(obj));
        }

        Task HandleMessageAsync(string messageType, TMessage message);
    }
}
