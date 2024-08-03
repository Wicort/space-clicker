using Items;
using System;
using System.Collections.Generic;

namespace Items
{
    public class ItemService
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
                ItemRarity.COMMON
            ); 
            _items.Add( item );

            item = new Item(
                "TestDrone",
                "Test Drone",
                ItemType.DRONE,
                "Descritrion for test Drone",
                null,
                ItemRarity.COMMON);
            _items.Add(item);

            item = new Item(
                "TestEngine",
                "Test Engine",
                ItemType.ENGINE,
                "Descritrion for test Engine",
                null,
                ItemRarity.COMMON);
            _items.Add(item);

            item = new Item(
                "TestTouret",
                "Test Touret",
                ItemType.TOURET,
                "Descritrion for test Touret",
                null,
                ItemRarity.COMMON);
            _items.Add(item);
        }

        internal Item GetCommonItemByType(ItemType moduleItemType)
        {
            return _items.Find(item => item.ItemType == moduleItemType);
        }
    }
}
