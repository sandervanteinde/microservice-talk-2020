using System.Threading.Tasks;

namespace Shared.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishMessageAsync<T>(string messageType, T value);
    }
}
