using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Messaging
{
    internal class QueueName
    {
        public string Name { get; }
        public QueueName(string name)
        {
            Name = name;
        }
    }
}
