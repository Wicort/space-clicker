using Services;
using UnityEngine;

namespace Inventory
{
    public interface IInventoryService : IService
    {
        AddItemsToInventoryGridResult AddItems(string ownerId, string itemId, int amount = 1);
        AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, Vector2Int slotCoords, string itemId, int amount = 1);
        IReadOnlyInventoryGrid GetInventory(string ownerId);
        bool Has(string ownerId, string itemId, int amount = 1);
        InventoryGrid RegisterInventory(InventoryGridData inventoryData);
        RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, string itemId, int amount = 1);
        RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, Vector2Int slotCoords, string itemId, int amount = 1);
    }
}