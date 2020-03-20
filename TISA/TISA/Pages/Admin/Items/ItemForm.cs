using System.ComponentModel.DataAnnotations;
using TISA.Models;

namespace TISA.Pages.Admin.Items
{
    public class ItemForm
    {
        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int BuyPrice { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int SellPrice { get; set; }

        [Required]
        public bool AvailableAtShop { get; set; }

        public static ItemForm FromItem(Item item) => new ItemForm
        {
            BuyPrice = item.BuyPrice,
            SellPrice = item.SellPrice,
            Name = item.Name,
            AvailableAtShop = item.AvailableAtShop
        };

        public Item ToItem() => new Item
        {
            BuyPrice = BuyPrice,
            AvailableAtShop = AvailableAtShop,
            Name = Name,
            SellPrice = SellPrice
        };
    }
}
