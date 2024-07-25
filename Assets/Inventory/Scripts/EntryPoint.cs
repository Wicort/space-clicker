using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Inventory
{
    public class EntryPoint: MonoBehaviour
    {
        public InventoryGridView _view;
        public InventoryService _inventoryService;

        private void Start()
        {
            _inventoryService = new InventoryService();

            var ownerId = "Player";

            var inventoryData = CreateTestInventory(ownerId);

            var playerInventory = _inventoryService.RegisterInventory(inventoryData);
            _view.Setup(playerInventory);

            var addedResult = _inventoryService.AddItems(ownerId, "TouretPart", 20);
            Debug.Log($"Items added. ItemId: TouretPart, amount to add: 20, amount added: {addedResult.ItemsAddedAmount}");

            addedResult = _inventoryService.AddItems(ownerId, "DronePart", 112);
            Debug.Log($"Items added. ItemId: DronePart, amount to add: 112, amount added: {addedResult.ItemsAddedAmount}");

            addedResult = _inventoryService.AddItems(ownerId, "MechanicGunPart", 30);
            Debug.Log($"Items added. ItemId: MechanicGunPart, amount to add: 30, amount added: {addedResult.ItemsAddedAmount}");

            _view.Print();

            var removeResult = _inventoryService.RemoveItems(ownerId, "TouretPart",10);
            Debug.Log($"Items removed. ItemId: TouretPart, amount to remove: 10, success: {removeResult.Success}");

            _view.Print();

            removeResult = _inventoryService.RemoveItems(ownerId, "TouretPart", 15);
            Debug.Log($"Items removed. ItemId: TouretPart, amount to remove: 15, success: {removeResult.Success}");

            _view.Print();

            
        }

        private InventoryGridData CreateTestInventory(string ownerId)
        {
            var size = new Vector2Int(3, 4);
            var createdInventorySlots = new List<InventorySlotData>();
            var length = size.x * size.y;
            for (var i = 0;i < length;i++)
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
