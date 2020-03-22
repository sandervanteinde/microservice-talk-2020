using System;

namespace ItemService.Database
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public bool AvailableAtShop { get; set; }
    }
}
