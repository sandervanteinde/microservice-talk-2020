using System.Threading.Tasks;

namespace Shared.Messaging
{
    /// <summary>
    /// Publishes a message on a message broker
    /// </summary>
    public interface IMessagePublisher
    {
        /// <summary>
        /// Publishes a message on a message broker
        /// </summary>
        /// <typeparam name="T">The type of the message to post</typeparam>
        /// <param name="messageType">Required identifying message type of the message. This can be used on the receiving end to identify this message. Should always be unique in your solution</param>
        /// <param name="value">The message to send</param>
        Task PublishMessageAsync<T>(string messageType, T value);
    }
}
