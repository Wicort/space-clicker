﻿using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class EntryPoint: MonoBehaviour
    {
        [SerializeField] private ScreenView _screenView;

        private const string OWNER_1 = "Player";
        private const string OWNER_2 = "Chest";
        private readonly string[] _itemIds = { "TouretPart", "DrontPart", "MechanicGunPart" };

        private InventoryService _inventoryService;
        private ScreenController _screenController;
        private string _cachedOwnerId;

        private void Start()
        {
            _inventoryService = new InventoryService();

            var inventoryDataPlayer = CreateTestInventory(OWNER_1);
            _inventoryService.RegisterInventory(inventoryDataPlayer);

            var inventoryDataChest = CreateTestInventory(OWNER_2);
            _inventoryService.RegisterInventory(inventoryDataChest);

            _screenController = new ScreenController(_inventoryService, _screenView);
            _screenController.OpenInventory(OWNER_1);
            _cachedOwnerId = OWNER_1;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _screenController.OpenInventory(OWNER_1);
                _cachedOwnerId = OWNER_1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _screenController.OpenInventory(OWNER_2);
                _cachedOwnerId = OWNER_2;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                var rIndex = Random.Range(0, _itemIds.Length);
                var rItemId = _itemIds[rIndex];
                var rAmount = Random.Range(0, 50);
                var result = _inventoryService.AddItems(_cachedOwnerId, rItemId, rAmount);

                Debug.Log($"Item added: ${rItemId}. Amount added: {result.ItemsAddedAmount}");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                var rIndex = Random.Range(0, _itemIds.Length);
                var rItemId = _itemIds[rIndex];
                var rAmount = Random.Range(0, 50);
                var result = _inventoryService.RemoveItems(_cachedOwnerId, rItemId, rAmount);

                Debug.Log($"Item removed: ${rItemId}. Truying to remove: {result.ItemsToRemoveAmount}. Success: {result.Success}");
            }
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
