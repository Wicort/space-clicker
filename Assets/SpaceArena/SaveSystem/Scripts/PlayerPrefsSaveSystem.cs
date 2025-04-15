using Assets.Services;
using Inventory;
using Items;
using System;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

namespace Assets.SpaceArena.SaveSystem.Scripts
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string IS_BOSS_FAILED = "IsBossFailed";
        private const string LEVEL = "Level";
        private const string CURRENCY = "Currency";
        private const string IS_SOUNDS_OFF = "IsSoundsOff";
        private const string IS_MUSIC_OFF = "IsMusidOff";
        private IInventoryService _inventory;
        private IItemService _itemService;
        private GameData _gameData;

        public PlayerPrefsSaveSystem(IInventoryService inventory, IItemService itemService)
        {
            _inventory = inventory;
            _itemService = itemService;
        }

        public GameData LoadGame()
        {
            _gameData = new GameData();

            if (_gameData.Settings == null)
                _gameData.Settings = new SettingsData();

            _gameData.Settings.IsSoundMute = LoadBool(IS_SOUNDS_OFF);
            _gameData.Settings.IsMusicMute = LoadBool(IS_MUSIC_OFF);

            //_gameData.SetIsBossFailed(LoadInt(IS_BOSS_FAILED) == 1);
            _gameData.SetIsBossFailed(false);
            _gameData.SetLevel(LoadInt(LEVEL));
            Debug.Log($"Load level {_gameData.Level}");
            _gameData.AddCurrency(LoadFloat(CURRENCY));

            _gameData.Module0Lvl = LoadInt("Module0Lvl");
            if (_gameData.Module0Lvl == 0) _gameData.Module0Lvl = 1;
            _gameData.Module1Lvl = LoadInt("Module1Lvl");
            _gameData.Module2Lvl = LoadInt("Module2Lvl");

            _gameData.Module0Rarity = PlayerPrefs.HasKey("Module0Rarity") ? Enum.Parse<ItemRarity>(LoadString("Module0Rarity")) : ItemRarity.COMMON;
            _gameData.Module1Rarity = PlayerPrefs.HasKey("Module1Rarity") ? Enum.Parse<ItemRarity>(LoadString("Module1Rarity")) : ItemRarity.COMMON;
            _gameData.Module2Rarity = PlayerPrefs.HasKey("Module2Rarity") ? Enum.Parse<ItemRarity>(LoadString("Module2Rarity")) : ItemRarity.COMMON;

            _gameData.DroneIsReady = LoadBool("DroneIsReady");

            _inventory.ClearInventory("Player");
            var size = _inventory.GetInventory("Player").Size;
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    if (PlayerPrefs.HasKey($"Inv_{x}_{y}_id") && PlayerPrefs.HasKey($"Inv_{x}_{y}_amount"))
                    {
                        string itemId = LoadString($"Inv_{x}_{y}_id");
                        if (itemId != null)
                        {
                            int amount = LoadInt($"Inv_{x}_{y}_amount");
                            _inventory.AddItems("Player", itemId, amount);
                        }
                    }
                }
            }
            string lastPlayedTimeString = LoadString("LastPlayedTime", DateTime.Now.ToString());
            if (lastPlayedTimeString != null && (lastPlayedTimeString != ""))
                _gameData.LastPlayedTime = DateTime.Parse(lastPlayedTimeString);

            return _gameData;

        }

        public void SaveGame()
        {
            Debug.Log("=================SaveGame()");
            PlayerPrefs.SetString("LastPlayedTime", DateTime.Now.ToString());

            PlayerPrefs.SetInt(IS_SOUNDS_OFF, _gameData.Settings.IsSoundMute ? 1 : 0);
            PlayerPrefs.SetInt(IS_MUSIC_OFF, _gameData.Settings.IsMusicMute ? 1 : 0);

            PlayerPrefs.SetInt(LEVEL, _gameData.Level);
            Debug.Log($"Save level {_gameData.Level}");
            /*if (_gameData.Level % 10 == 0)
            {
                PlayerPrefs.SetInt(IS_BOSS_FAILED, 1);
            }
            else
            {
                PlayerPrefs.SetInt(IS_BOSS_FAILED, _gameData.IsBossFailed ? 1 : 0);
            }*/
            PlayerPrefs.SetFloat(CURRENCY, _gameData.Currency);

            if (_gameData.Modules == null) return;

            int i = 0;
            foreach (ActiveUpgrade upg in _gameData.Modules)
            {
                PlayerPrefs.SetInt("Module" + i + "Lvl", upg.CurrentLevel);
                PlayerPrefs.SetString("Module" + i + "Rarity", upg.GetModule().GetRarity().ToString());
                i++;
            }
            PlayerPrefs.SetInt("DroneIsReady", _gameData.DroneIsReady ? 1 : 0);

            if (_inventory != null)
            {
                Vector2Int size = _inventory.GetInventory("Player").Size;
                IReadOnlyInventorySlot[,] slots = _inventory.GetInventory("Player").GetSlots();
                for (var x = 0; x < size.x; x++)
                {
                    for (var y = 0; y < size.y; y++)
                    {
                        IReadOnlyInventorySlot slot = slots[x, y];
                        if (slot.ItemId != null && slot.ItemId != "")
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

            PlayerPrefs.Save();
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

        private bool LoadBool(string key)
        {
            int val = LoadInt(key);
            return val == 0 ? false : true;
        }

        private float LoadFloat(string key)
        {
            if (key == null) return 0f;

            if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetFloat(key, 0f);

            return 0f;
        }

        public void SaveSettings(SettingsData data)
        {
            _gameData.Settings = data;
            SaveGame();
        }
    }
}
