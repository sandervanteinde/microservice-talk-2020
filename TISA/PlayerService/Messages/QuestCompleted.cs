using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerService.Messages
{
    public class QuestCompleted
    {
        public Guid PlayerId { get; set; }
        public Quest Quest { get; set; }
    }
}