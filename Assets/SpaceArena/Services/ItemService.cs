using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.SpaceArena.Scripts.Infrastructure.Localization;
using Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Services
{
    public class ItemService : IItemService
    {
        private List<Item> _items;
        private ILocalizationService _localizationService;

        public ItemService(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            SetItemsList();
        }

        public Item GetItemInfo(string itemId)
        {
            return _items.Find(item => item.Id == itemId);
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

        public Item GetItemByTypeAndRariry(ItemType moduleItemType, ItemRarity rarity)
        {
            var rarityItems = _items.FindAll(item => item.Rarity == rarity && item.ItemType == moduleItemType);
            var index = Random.Range(0, rarityItems.Count);
            return rarityItems[index];
        }

        public void SetItemsList()
        {
            _items = new List<Item>();
            //Common
            Item item = new Item(
                "CommonGun",
                _localizationService.GetItemName("CommonGun"),
                ItemType.GUN,
                _localizationService.GetItemDescription("CommonGun"),
                Resources.Load<Sprite>(AssetPath.CommonGunPath),
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "CommonDrone",
                _localizationService.GetItemName("CommonDrone"),
                ItemType.DRONE,
                _localizationService.GetItemDescription("CommonDrone"),
                Resources.Load<Sprite>(AssetPath.CommonDronePath),
                ItemRarity.COMMON,
                1
            );
            _items.Add(item);

            item = new Item(
                "CommonTouret",
                _localizationService.GetItemName("CommonTouret"),
                ItemType.TOURET,
                _localizationService.GetItemDescription("CommonTouret"),
                Resources.Load<Sprite>(AssetPath.CommonTouretPath),
                ItemRarity.COMMON,
                10
            );
            _items.Add(item);

            //UNCOMMON
            item = new Item(
                "UNCOMMONGun",
                _localizationService.GetItemName("UNCOMMONGun"),
                ItemType.GUN,
                _localizationService.GetItemDescription("UNCOMMONGun"),
                Resources.Load<Sprite>(AssetPath.UncommonGunPath),
                ItemRarity.UNCOMMON,
                2
            );
            _items.Add(item);

            item = new Item(
                "UNCOMMONDrone",
                _localizationService.GetItemName("UNCOMMONDrone"),
                ItemType.DRONE,
                _localizationService.GetItemDescription("UNCOMMONDrone"),
                Resources.Load<Sprite>(AssetPath.UncommonDronePath),
                ItemRarity.UNCOMMON,
                2
            );
            _items.Add(item);

            item = new Item(
                "UNCOMMONTouret",
                _localizationService.GetItemName("UNCOMMONTouret"),
                ItemType.TOURET,
                _localizationService.GetItemDescription("UNCOMMONTouret"),
                Resources.Load<Sprite>(AssetPath.UncommonTouretPath),
                ItemRarity.UNCOMMON,
                20
            );
            _items.Add(item);

            //RARE
            item = new Item(
                "RAREGun",
                _localizationService.GetItemName("RAREGun"),
                ItemType.GUN,
                _localizationService.GetItemDescription("RAREGun"),
                Resources.Load<Sprite>(AssetPath.RareGunPath),
                ItemRarity.RARE,
                5
            );
            _items.Add(item);

            item = new Item(
                "RAREDrone",
                _localizationService.GetItemName("RAREDrone"),
                ItemType.DRONE,
                _localizationService.GetItemDescription("RAREDrone"),
                Resources.Load<Sprite>(AssetPath.RareDronePath),
                ItemRarity.RARE,
                5
            );
            _items.Add(item);

            item = new Item(
                "RARETouret",
                _localizationService.GetItemName("RARETouret"),
                ItemType.TOURET,
                _localizationService.GetItemDescription("RARETouret"),
                Resources.Load<Sprite>(AssetPath.RareTouretPath),
                ItemRarity.RARE,
                50
            );
            _items.Add(item);

            //EPIC
            item = new Item(
                "EPICGun",
                _localizationService.GetItemName("EPICGun"),
                ItemType.GUN,
                _localizationService.GetItemDescription("EPICGun"),
                Resources.Load<Sprite>(AssetPath.EpicGunPath),
                ItemRarity.EPIC,
                10
            );
            _items.Add(item);

            item = new Item(
                "EPICDrone",
                _localizationService.GetItemName("EPICDrone"),
                ItemType.DRONE,
                _localizationService.GetItemDescription("EPICDrone"),
                Resources.Load<Sprite>(AssetPath.EpicDronePath),
                ItemRarity.EPIC,
                10
            );
            _items.Add(item);

            item = new Item(
                "EPICTouret",
                _localizationService.GetItemName("EPICTouret"),
                ItemType.TOURET,
                _localizationService.GetItemDescription("EPICTouret"),
                Resources.Load<Sprite>(AssetPath.EpicTouretPath),
                ItemRarity.EPIC,
                100
            );
            _items.Add(item);

            //LEGENDARY
            item = new Item(
                "LEGENDARYGun",
                _localizationService.GetItemName("LEGENDARYGun"),
                ItemType.GUN,
                _localizationService.GetItemDescription("LEGENDARYGun"),
                Resources.Load<Sprite>(AssetPath.LegGunPath),
                ItemRarity.LEGENDARY,
                15
            );
            _items.Add(item);

            item = new Item(
                "LEGENDARYDrone",
                _localizationService.GetItemName("LEGENDARYDrone"),
                ItemType.DRONE,
                _localizationService.GetItemDescription("LEGENDARYDrone"),
                Resources.Load<Sprite>(AssetPath.LegDronePath),
                ItemRarity.LEGENDARY,
                15
            );
            _items.Add(item);

            item = new Item(
                "LEGENDARYTouret",
                _localizationService.GetItemName("LEGENDARYTouret"),
                ItemType.TOURET,
                _localizationService.GetItemDescription("LEGENDARYTouret"),
                Resources.Load<Sprite>(AssetPath.LegTouretPath),
                ItemRarity.LEGENDARY,
                150
            );
            _items.Add(item);

            //MYPHICAL
            item = new Item(
                "MYTHICALGun",
                _localizationService.GetItemName("MYTHICALGun"),
                ItemType.GUN,
                _localizationService.GetItemDescription("MYTHICALGun"),
                Resources.Load<Sprite>(AssetPath.MythGunPath),
                ItemRarity.MYTHICAL,
                20
            );
            _items.Add(item);

            item = new Item(
                "MYTHICALDrone",
                _localizationService.GetItemName("MYTHICALDrone"),
                ItemType.DRONE,
                _localizationService.GetItemDescription("MYTHICALDrone"),
                Resources.Load<Sprite>(AssetPath.MythDronePath),
                ItemRarity.MYTHICAL,
                20
            );
            _items.Add(item);

            item = new Item(
                "MYTHICALTouret",
                _localizationService.GetItemName("MYTHICALTouret"),
                ItemType.TOURET,
                _localizationService.GetItemDescription("MYTHICALTouret"),
                Resources.Load<Sprite>(AssetPath.MythTouretPath),
                ItemRarity.MYTHICAL,
                200
            );
            _items.Add(item);
        }

    }
}
