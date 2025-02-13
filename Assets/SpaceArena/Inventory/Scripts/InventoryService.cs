using Assets.SpaceArena.Scripts.Infrastructure.Localization;
using Services;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryService : IInventoryService
    {
        public readonly Dictionary<string, InventoryGrid> _inventoriesMap = new();
        private ILocalizationService _localizationService;

        public InventoryService() 
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            var inventoryDataPlayer = CreatePlayerInventory("Player");
            RegisterInventory(inventoryDataPlayer);
        }

        public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
        {
            var inventory = new InventoryGrid(inventoryData);
            _inventoriesMap[inventory.OwnerId] = inventory;

            return inventory;
        }

        public AddItemsToInventoryGridResult AddItems(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(itemId, amount);
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(
            string ownerId,
            Vector2Int slotCoords,
            string itemId,
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(slotCoords, itemId, amount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(itemId, amount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(
            string ownerId,
            Vector2Int slotCoords,
            string itemId,
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(slotCoords, itemId, amount);
        }

        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }

        public IReadOnlyInventoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }

        public void ClearInventory(string ownerId)
        {
            _inventoriesMap.Remove(ownerId);
            var inventoryDataPlayer = CreatePlayerInventory("Player");
            RegisterInventory(inventoryDataPlayer);
        }

        private InventoryGridData CreatePlayerInventory(string ownerId)
        {
            var size = new Vector2Int(3, 4);
            var createdInventorySlots = new List<InventorySlotData>();
            var length = size.x * size.y;
            for (var i = 0; i < length; i++)
            {
                createdInventorySlots.Add(new InventorySlotData());
            }

            var createdInventoryData = new InventoryGridData
            {
                OwnerId = ownerId,
                Size = size,
                Slots = createdInventorySlots
            };

            return createdInventoryData;
        }
    }
}
