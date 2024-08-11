using Assets.Scripts.AssetProvider;
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
                Resources.Load<Sprite>(AssetPath.CommonGunPath),
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "CommonDrone",
                "Common Drone",
                ItemType.DRONE,
                "Description for Common Drone",
                Resources.Load<Sprite>(AssetPath.CommonDronePath),
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "CommonTouret",
                "Common Touret",
                ItemType.TOURET,
                "Description for Common Touret",
                Resources.Load<Sprite>(AssetPath.CommonTouretPath),
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
                Resources.Load<Sprite>(AssetPath.UncommonGunPath),
                ItemRarity.UNCOMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "UNCOMMONDrone",
                "UNCOMMON Drone",
                ItemType.DRONE,
                "Description for UNCOMMON Drone",
                Resources.Load<Sprite>(AssetPath.UncommonDronePath),
                ItemRarity.UNCOMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "UNCOMMONTouret",
                "UNCOMMON Touret",
                ItemType.TOURET,
                "Description for UNCOMMON Touret",
                Resources.Load<Sprite>(AssetPath.UncommonTouretPath),
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
                Resources.Load<Sprite>(AssetPath.RareGunPath),
                ItemRarity.RARE,
                1
            );
            _items.Add(item);

            item = new Item(
                "RAREDrone",
                "RARE Drone",
                ItemType.DRONE,
                "Description for RARE Drone",
                Resources.Load<Sprite>(AssetPath.RareDronePath),
                ItemRarity.RARE,
                1
            );
            _items.Add(item);

            item = new Item(
                "RARETouret",
                "RARE Touret",
                ItemType.TOURET,
                "Description for RARE Touret",
                Resources.Load<Sprite>(AssetPath.RareTouretPath),
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
                Resources.Load<Sprite>(AssetPath.EpicGunPath),
                ItemRarity.EPIC,
                1
            );
            _items.Add(item);

            item = new Item(
                "EPICDrone",
                "EPIC Drone",
                ItemType.DRONE,
                "Description for EPIC Drone",
                Resources.Load<Sprite>(AssetPath.EpicDronePath),
                ItemRarity.EPIC,
                1
            );
            _items.Add(item);

            item = new Item(
                "EPICTouret",
                "EPIC Touret",
                ItemType.TOURET,
                "Description for EPIC Touret",
                Resources.Load<Sprite>(AssetPath.EpicTouretPath),
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
                Resources.Load<Sprite>(AssetPath.LegGunPath),
                ItemRarity.LEGENDARY,
                1
            );
            _items.Add(item);

            item = new Item(
                "LEGENDARYDrone",
                "LEGENDARY Drone",
                ItemType.DRONE,
                "Description for LEGENDARY Drone",
                Resources.Load<Sprite>(AssetPath.LegDronePath),
                ItemRarity.LEGENDARY,
                1
            );
            _items.Add(item);

            item = new Item(
                "LEGENDARYTouret",
                "LEGENDARY Touret",
                ItemType.TOURET,
                "Description for LEGENDARY Touret",
                Resources.Load<Sprite>(AssetPath.LegTouretPath),
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
                Resources.Load<Sprite>(AssetPath.MyphGunPath),
                ItemRarity.MYPHICAL,
                1
            );
            _items.Add(item);

            item = new Item(
                "MYPHICALDrone",
                "MYPHICAL Drone",
                ItemType.DRONE,
                "Description for MYPHICAL Drone",
                Resources.Load<Sprite>(AssetPath.MyphDronePath),
                ItemRarity.MYPHICAL,
                1
            );
            _items.Add(item);

            item = new Item(
                "MYPHICALTouret",
                "MYPHICAL Touret",
                ItemType.TOURET,
                "Description for MYPHICAL Touret",
                Resources.Load<Sprite>(AssetPath.MyphTouretPath),
                ItemRarity.MYPHICAL,
                10
            );
            _items.Add(item);
        }

    }
}
