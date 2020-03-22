using System;

namespace PlayerService.Messages
{
    public class ItemSold
    {
        public Item Item { get; set; }
        public Guid PlayerId { get; set; }
    }
}