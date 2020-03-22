using System;

namespace ItemService.Database
{
    public class PlayerItem
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}
