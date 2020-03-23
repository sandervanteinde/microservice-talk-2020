using System;
using System.ComponentModel.DataAnnotations;

namespace PlayerService.Database
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
    }
}
