
using Assets.Services;
using Inventory;
using Items;
using System;
using UnityEngine;

namespace Assets.SaveSystem.Scripts
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string IS_BOSS_FAILED = "IsBossFailed";
        private const string LEVEL = "Level";
        private const string CURRENCY = "Currency";
        private IInventoryService _inventory;
        private IItemService _itemService;

        public PlayerPrefsSaveSystem (IInventoryService inventory, IItemService itemService)
        {
            _inventory = inventory;
            _itemService = itemService;
        }

        public GameData Load()
        {
            GameData gameData = new GameData();

            gameData.SetIsBossFailed(LoadInt(IS_BOSS_FAILED) == 1);
            gameData.SetLevel(LoadInt(LEVEL));
            gameData.AddCurrency(LoadFloat(CURRENCY));

            gameData.Module0Lvl = LoadInt("Module0Lvl");
            if (gameData.Module0Lvl == 0) gameData.Module0Lvl = 1;
            gameData.Module1Lvl = LoadInt("Module1Lvl");
            gameData.Module2Lvl = LoadInt("Module2Lvl");

            gameData.Module0Rarity = PlayerPrefs.HasKey("Module0Rarity") ? Enum.Parse<ItemRarity>(LoadString("Module0Rarity")) : ItemRarity.COMMON;
            gameData.Module1Rarity = PlayerPrefs.HasKey("Module1Rarity") ? Enum.Parse<ItemRarity>(LoadString("Module1Rarity")) : ItemRarity.COMMON;
            gameData.Module2Rarity = PlayerPrefs.HasKey("Module2Rarity") ? Enum.Parse<ItemRarity>(LoadString("Module2Rarity")) : ItemRarity.COMMON;

            var size = _inventory.GetInventory("Player").Size;
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    if (PlayerPrefs.HasKey($"Inv_{x}_{y}_id") && PlayerPrefs.HasKey($"Inv_{x}_{y}_amount"))
                    {
                        string itemId = LoadString($"Inv_{x}_{y}_id");
                        if (itemId != null)
                            _inventory.AddItems("Player", itemId, LoadInt($"Inv_{x}_{y}_amount"));
                    }
                }
            }
            string lastPlayedTimeString = LoadString("LastPlayedTime", DateTime.UtcNow.ToString());
            if (lastPlayedTimeString != null)
                gameData.LastPlayedTime = DateTime.Parse(lastPlayedTimeString);

            return gameData;

        }

        public void Save(GameData data)
        {
            PlayerPrefs.SetString("LastPlayedTime", DateTime.UtcNow.ToString());

            if (data.Level % 10 == 0)
            {
                PlayerPrefs.SetInt(LEVEL, data.Level-2);
                PlayerPrefs.SetInt(IS_BOSS_FAILED, 1);
            }
            else
            {
                PlayerPrefs.SetInt(LEVEL, data.Level-1);
                PlayerPrefs.SetInt(IS_BOSS_FAILED, data.IsBossFailed ? 1 : 0);
            }
            PlayerPrefs.SetFloat(CURRENCY, data.Currency);

            if (data.Modules == null) return;

            int i = 0;
            foreach(ActiveUpgrade upg in data.Modules)
            {
                PlayerPrefs.SetInt("Module" + i + "Lvl", upg.CurrentLevel);
                PlayerPrefs.SetString("Module" + i + "Rarity", upg.GetModule().GetRarity().ToString());
                i++;
            }

            if (_inventory != null)
            {
                var size = _inventory.GetInventory("Player").Size;
                var slots = _inventory.GetInventory("Player").GetSlots();
                for (var x = 0; x < size.x; x++)
                {
                    for (var y = 0; y < size.y; y++)
                    {
                        var slot = slots[x, y];
                        if (slot.ItemId != null)
                        {
                            var item = _itemService.GetItemInfo(slot.ItemId);
                            PlayerPrefs.SetString($"Inv_{x}_{y}_id", slot.ItemId);
                            PlayerPrefs.SetInt($"Inv_{x}_{y}_amount", slot.Amount);
                        }
                        else
                        {
                            PlayerPrefs.SetString($"Inv_{x}_{y}_id", null);
                            PlayerPrefs.SetInt($"Inv_{x}_{y}_amount", 0);
                        }
                    }
                }
            }
        }

        private string LoadString(string key, string defVal = null)
        {
            if (key == null) return null;

            if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetString(key, defVal);

            return null;
        }

        private int LoadInt(string key)
        {
            if (key == null) return 0;

            if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetInt(key, 0);

            return 0;
        }

        private float LoadFloat(string key)
        {
            if (key == null) return 0f;

            if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetFloat(key, 0f);

            return 0f;
        }
    }
}
