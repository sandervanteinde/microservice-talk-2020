using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TISA.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
    }
}
