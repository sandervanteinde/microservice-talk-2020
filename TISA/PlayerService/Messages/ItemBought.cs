using System;

namespace PlayerService.Messages
{
    public class ItemBought
    {
        public Item Item { get; set; }
        public Guid PlayerId { get; set; }
    }
}