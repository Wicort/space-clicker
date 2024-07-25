﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {

        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        public event Action<Vector2Int> SizeChanged;

        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;
            var size = _data.Size;
            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var index = i * size.y + j;
                    var slotData = data.Slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);

                    _slotsMap[position] = slot;
                }
            }
        }

        public string OwnerId => _data.OwnerId;
        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                {
                    _data.Size = value;
                    SizeChanged?.Invoke(value);
                }
            }
        }

        public int GetAmount(string itemId)
        {
            var amount = 0;
            var slots = _data.Slots;

            foreach (var slot in slots)
            {
                if (slot.ItemId == itemId)
                {
                    amount += slot.Amount;
                }
            }
             
            return amount;
        }

        public bool Has(string itemId, int amount)
        {
            var amountExist = GetAmount(itemId);
            return amountExist >= amount;
        }

        public void SwitchSlots(Vector2Int slotCoordsA, Vector2Int slotCoordsB)
        {
            var slotA = _slotsMap[slotCoordsA];
            var slotB = _slotsMap[slotCoordsB];

            var tempSlotItemId = slotA.ItemId;
            var tempSlotAmount = slotA.Amount;

            slotA.ItemId = slotB.ItemId;
            slotA.Amount = slotB.Amount;

            slotB.ItemId = tempSlotItemId;
            slotB.Amount = tempSlotAmount;
        }

        public void SetSize(Vector2Int newSize)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyInventorySlot[,] GetSlots()
        {
            var array = new IReadOnlyInventorySlot[Size.x, Size.y];

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }

            return array;
        }

        public AddItemsToInventoryGridResult AddItems(string itemId, int amount = 1)
        {
            var remainingAmount = amount;
            var itemsAddedToSlotWithSameItemsAmount = AddToSlotWithSameItems(itemId, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
            {
                return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedToSlotWithSameItemsAmount);
            }

            var itemsAddedToAvailableSlotAmount = AddItemsAddedToAvailableSlot(itemId, remainingAmount, out remainingAmount);
            var totalAddedAmount = itemsAddedToSlotWithSameItemsAmount + itemsAddedToAvailableSlotAmount;

            return new AddItemsToInventoryGridResult(OwnerId, amount, totalAddedAmount);

        }


        public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty)
            {
                slot.ItemId = itemId;
            }

            var itemSlotCapacity = GetItemSlotCapacity(itemId);

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemId, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
            }

            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
        }


        public RemoveItemsFromInventoryGridResult RemoveItems(string itemId, int amount = 1)
        {
            if (!Has(itemId, amount)) {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

            var amountToRemove = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var slotCoords = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoords];

                    if (slot.ItemId != itemId)
                    {
                        continue;
                    }

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;

                        RemoveItems(slotCoords, itemId, slot.Amount);
                    }
                    else
                    {
                        RemoveItems(slotCoords, itemId, amountToRemove);

                        return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception("Something went wrong, couldn't remove some items");
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];

            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
            {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

            slot.Amount -= amount;

            if (slot.Amount == 0)
            {
                slot.ItemId = null;
            }

            return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
        }
        

        private int AddToSlotWithSameItems(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.IsEmpty)
                    {
                        continue;
                    }

                    var slotItemCapacity = GetItemSlotCapacity(itemId);
                    if (slot.Amount >= slotItemCapacity)
                    {
                        continue;
                    }

                    if (slot.ItemId != itemId)
                    {
                        continue;
                    }

                    var newValue = slot.Amount + remainingAmount;
                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }
            return itemsAddedAmount;
        }

        private int AddItemsAddedToAvailableSlot(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (!slot.IsEmpty)
                    {
                        continue;
                    }

                    slot.ItemId = itemId;
                    var newValue = remainingAmount;
                    var slotItemCapaclty = GetItemSlotCapacity(slot.ItemId);

                    if (newValue > slotItemCapaclty)
                    {
                        remainingAmount = newValue - slotItemCapaclty;
                        var itemsToAddAmount = slotItemCapaclty;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapaclty;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }
            return itemsAddedAmount;
        }

        private int GetItemSlotCapacity(string itemId)
        {
            return 99;
        }
    }
}
