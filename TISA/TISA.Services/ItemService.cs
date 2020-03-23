using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class ItemService : IItemService
    {
        private readonly IPlayerService _playerService;

        public ItemService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Task CreateItemAsync(Item item)
        {
            return "https://localhost:7701/Item".PostJsonAsync(item);
        }

        public Task DeleteItemByIdAsync(Guid itemId)
        {
            return $"https://localhost:7701/Item/{itemId}".DeleteAsync();
        }

        public Task<ICollection<Item>> GetAllItemsAsync()
        {
            return "https://localhost:7701/Item".GetJsonAsync<ICollection<Item>>();
        }

        public Task<Item> GetItemByIdAsync(Guid itemId)
        {
            return $"https://localhost:7701/Item/{itemId}".GetJsonAsync<Item>();
        }

        public Task UpdateItemAsync(Guid itemId, Item item)
        {
            return $"https://localhost:7701/Item/{itemId}".PutJsonAsync(item);
        }

        public Task<ICollection<Item>> GetItemsForPlayerAsync()
        {
            return $"https://localhost:7701/Inventory/{_playerService.Player.Id}".GetJsonAsync<ICollection<Item>>();
        }

        public Task<ICollection<Item>> GetShopItemsAsync()
        {
            return $"https://localhost:7701/Shop".GetJsonAsync<ICollection<Item>>();
        }

        public Task SellPlayerItem(Guid itemId)
        {
            return $"https://localhost:7701/Inventory/{_playerService.Player.Id}/Sell".PostJsonAsync(itemId);
        }

        public Task BuyPlayerItem(Guid itemId)
        {
            return $"https://localhost:7701/Inventory/{_playerService.Player.Id}/Buy".PostJsonAsync(itemId);
        }
    }
}
