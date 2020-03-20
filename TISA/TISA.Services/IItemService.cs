using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    public interface IItemService
    {
        Task CreateItemAsync(Item item);
        Task<ICollection<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(Guid itemId);
        Task UpdateItemAsync(Guid itemId, Item item);
        Task DeleteItemByIdAsync(Guid questId);
        Task<ICollection<Item>> GetItemsForPlayerAsync();
        Task<ICollection<Item>> GetShopItemsAsync();
        Task SellPlayerItem(Guid itemId);
        Task BuyPlayerItem(Guid itemId);
    }
}