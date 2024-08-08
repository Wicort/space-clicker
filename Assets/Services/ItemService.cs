using Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Services
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

        public void SetTestItemsList()
        {
            _items = new List<Item>();
            //Common
            Item item = new Item(
                "CommonGun",
                "Common Gun",
                ItemType.GUN,
                "Description for Common Gun",
                null,
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "CommonDrone",
                "Common Drone",
                ItemType.DRONE,
                "Description for Common Drone",
                null,
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "CommonTouret",
                "Common Touret",
                ItemType.TOURET,
                "Description for Common Touret",
                null,
                ItemRarity.COMMON,
                10
            );
            _items.Add(item);

            //UNCOMMON
            item = new Item(
                "UNCOMMONGun",
                "UNCOMMON Gun",
                ItemType.GUN,
                "Description for UNCOMMON Gun",
                null,
                ItemRarity.UNCOMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "UNCOMMONDrone",
                "UNCOMMON Drone",
                ItemType.DRONE,
                "Description for UNCOMMON Drone",
                null,
                ItemRarity.UNCOMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "UNCOMMONTouret",
                "UNCOMMON Touret",
                ItemType.TOURET,
                "Description for UNCOMMON Touret",
                null,
                ItemRarity.UNCOMMON,
                10
            );
            _items.Add(item);

            //RARE
            item = new Item(
                "RAREGun",
                "RARE Gun",
                ItemType.GUN,
                "Description for RARE Gun",
                null,
                ItemRarity.RARE,
                1
            );
            _items.Add(item);

            item = new Item(
                "RAREDrone",
                "RARE Drone",
                ItemType.DRONE,
                "Description for RARE Drone",
                null,
                ItemRarity.RARE,
                1
            );
            _items.Add(item);

            item = new Item(
                "RARETouret",
                "RARE Touret",
                ItemType.TOURET,
                "Description for RARE Touret",
                null,
                ItemRarity.RARE,
                10
            );
            _items.Add(item);

            //EPIC
            item = new Item(
                "EPICGun",
                "EPIC Gun",
                ItemType.GUN,
                "Description for EPIC Gun",
                null,
                ItemRarity.EPIC,
                1
            );
            _items.Add(item);

            item = new Item(
                "EPICDrone",
                "EPIC Drone",
                ItemType.DRONE,
                "Description for EPIC Drone",
                null,
                ItemRarity.EPIC,
                1
            );
            _items.Add(item);

            item = new Item(
                "EPICTouret",
                "EPIC Touret",
                ItemType.TOURET,
                "Description for EPIC Touret",
                null,
                ItemRarity.EPIC,
                10
            );
            _items.Add(item);

            //LEGENDARY
            item = new Item(
                "LEGENDARYGun",
                "LEGENDARY Gun",
                ItemType.GUN,
                "Description for LEGENDARY Gun",
                null,
                ItemRarity.LEGENDARY,
                1
            );
            _items.Add(item);

            item = new Item(
                "LEGENDARYDrone",
                "LEGENDARY Drone",
                ItemType.DRONE,
                "Description for LEGENDARY Drone",
                null,
                ItemRarity.LEGENDARY,
                1
            );
            _items.Add(item);

            item = new Item(
                "LEGENDARYTouret",
                "LEGENDARY Touret",
                ItemType.TOURET,
                "Description for LEGENDARY Touret",
                null,
                ItemRarity.LEGENDARY,
                10
            );
            _items.Add(item);

            //MYPHICAL
            item = new Item(
                "MYPHICALGun",
                "MYPHICAL Gun",
                ItemType.GUN,
                "Description for MYPHICAL Gun",
                null,
                ItemRarity.MYPHICAL,
                1
            );
            _items.Add(item);

            item = new Item(
                "MYPHICALDrone",
                "MYPHICAL Drone",
                ItemType.DRONE,
                "Description for MYPHICAL Drone",
                null,
                ItemRarity.MYPHICAL,
                1
            );
            _items.Add(item);

            item = new Item(
                "MYPHICALTouret",
                "MYPHICAL Touret",
                ItemType.TOURET,
                "Description for MYPHICAL Touret",
                null,
                ItemRarity.MYPHICAL,
                10
            );
            _items.Add(item);
        }

    }
}
