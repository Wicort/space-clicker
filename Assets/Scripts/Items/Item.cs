using System;
using UnityEngine;

namespace Items
{
    public interface Item
    {
        public string Name { get; }
        public ItemType ItemType { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public void Equip();
        public void Remove();
    }
}
