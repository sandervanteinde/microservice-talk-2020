using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    internal class ItemService : IItemService
    {
        static ItemService()
        {
            var item = new Item { Id = Guid.NewGuid(), BuyPrice = 50, Name = "Wooden Sword", SellPrice = 25, AvailableAtShop = true };
            var item2 = new Item { Id = Guid.NewGuid(), BuyPrice = 100, Name = "Iron Sword", SellPrice = 50, AvailableAtShop = true };
            _items = new List<Item> { item, item2 };
        }


        private static List<Item> _items;
        private static Dictionary<Player, List<Guid>> _itemsPerPlayer = new Dictionary<Player, List<Guid>>();
        private readonly IPlayerService _playerService;

        public ItemService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Task CreateItemAsync(Item item)
        {
            item.Id = Guid.NewGuid();
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task DeleteItemByIdAsync(Guid itemId)
        {
            var itemIndex = _items.FindIndex(item => item.Id == itemId);
            if (itemIndex >= 0)
            {
                _items.RemoveAt(itemIndex);
            }

            return Task.CompletedTask;
        }

        public Task<ICollection<Item>> GetAllItemsAsync()
        {
            return Task.FromResult<ICollection<Item>>(_items.ToArray());
        }

        public Task<Item> GetItemByIdAsync(Guid itemId)
        {
            return Task.FromResult(_items.Find(item => item.Id == itemId));
        }

        public async Task UpdateItemAsync(Guid itemId, Item item)
        {
            var existingItem = await GetItemByIdAsync(itemId);
            _items.Remove(existingItem);
            item.Id = itemId;
            _items.Add(item);
        }

        public Task<ICollection<Item>> GetItemsForPlayerAsync()
        {
            var inventory = GetInventoryForPlayer();
            ICollection<Item> items = inventory
                .Select(itemId => _items.Find(item => item.Id == itemId))
                .ToList();

            return Task.FromResult(items);
        }

        public List<Guid> GetInventoryForPlayer()
        {
            if (!_playerService.IsPlayerDefined)
            {
                throw new InvalidOperationException("Attempted to get player information, but no player was defined");
            }
            if (!_itemsPerPlayer.TryGetValue(_playerService.Player, out var inventory))
            {
                _itemsPerPlayer.Add(_playerService.Player, inventory = new List<Guid>());
            }

            return inventory;
        }

        public Task<ICollection<Item>> GetShopItemsAsync()
        {
            ICollection<Item> items = _items.Where(item => item.AvailableAtShop).ToList();
            return Task.FromResult(items);
        }

        public async Task SellPlayerItem(Guid itemId)
        {
            var inventory = GetInventoryForPlayer();
            var item = await GetItemByIdAsync(itemId);
            if (item != null)
            {
                var index = inventory.FindIndex(i => item.Id == i);
                if (index >= 0)
                {
                    inventory.RemoveAt(index);
                    var player = _playerService.Player;
                    player.Gold += item.SellPrice;
                }
            }
        }

        public async Task BuyPlayerItem(Guid itemId)
        {
            var inventory = GetInventoryForPlayer();
            var item = await GetItemByIdAsync(itemId);
            if (item != null)
            {
                var player = _playerService.Player;
                inventory.Add(item.Id);
                player.Gold -= item.BuyPrice;
            }
        }
    }
}
