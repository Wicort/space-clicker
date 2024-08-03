using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class Item
    {
        public string Id { get; }
        public string Name { get; }
        public ItemType ItemType { get; }
        public string Description { get; }
        public Sprite Icon { get; }

        public ItemRarity Rarity { get; }

        public Item(string itemId, string name, ItemType itemType, string description, Sprite icon, ItemRarity rarity)
        {
            Id = itemId;
            Name = name;
            ItemType = itemType;
            Description = description;
            Icon = icon;
            Rarity = rarity;
        }

        public void Equip()
        {

        }
        public void Remove()
        {

        }
    }
}
