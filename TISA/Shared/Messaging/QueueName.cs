namespace Shared.Messaging
{
    /// <summary>
    /// Helper class to help finding the name of the queue within this application
    /// </summary>
    internal class QueueName
    {
        public string Name { get; }
        public QueueName(string name)
        {
            Name = name;
        }
    }
}
