using System;
using UnityEngine;

namespace Inventory
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        private readonly InventorySlotData _data;

        public event Action<string> ItemIdChanged;
        public event Action<int> ItemAmountChanged;
        public event Action<Sprite> ItemSpriteChanged;

        public string ItemId 
        { 
            get => _data.ItemId; 
            set
            {
                if (_data.ItemId != value)
                {
                    _data.ItemId = value;
                    ItemIdChanged?.Invoke(value);
                }
            }
        }
        public int Amount 
        {
            get => _data.Amount;
            set
            {
                if (_data.Amount != value)
                {
                    _data.Amount = value;
                    ItemAmountChanged?.Invoke(value);
                }
            }
        }
        public bool IsEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);

        public Sprite ItemSprite
        {
            get => _data.ItemSprite;
            set
            {
                if (_data.ItemSprite != value)
                {
                    _data.ItemSprite = value;
                    ItemSpriteChanged?.Invoke(value);
                }
            }
        }

        public InventorySlot(InventorySlotData data)
        {
            _data = data;
        }
    }
}
