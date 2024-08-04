using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemService : IItemService
    {
        private List<Item> _items;
        public Item GetItemInfo(string itemId)
        {
            return _items.Find(item => item.Id == itemId);
        }

        public ItemService()
        {
            SetTestItemsList();
        }

        public void SetTestItemsList()
        {
            _items = new List<Item>();

            Item item = new Item(
                "TestGun",
                "Test Gun",
                ItemType.GUN,
                "Descritrion for test Gun",
                null,
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "TestDrone",
                "Test Drone",
                ItemType.DRONE,
                "Descritrion for test Drone",
                null,
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "TestEngine",
                "Test Engine",
                ItemType.ENGINE,
                "Descritrion for test Engine",
                null,
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "TestTouret",
                "Test Touret",
                ItemType.TOURET,
                "Descritrion for test Touret",
                null,
                ItemRarity.COMMON,
                10
            );
            _items.Add(item);
        }

        public Item GetCommonItemByType(ItemType moduleItemType)
        {
            return _items.Find(item => item.ItemType == moduleItemType);
        }

        public Item GetRandomItemByRarity(ItemRarity rarity)
        {
            var rarityItems = _items.FindAll(item => item.Rarity == rarity);
            var index = Random.Range(0, rarityItems.Count);
            return rarityItems[index];
        }
    }
}
