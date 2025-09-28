using Assets.Services;
using Inventory;
using Items;
using System;
using UnityEngine;
using YG;

namespace Assets.SpaceArena.SaveSystem.Scripts
{
    public class YandexCloudSaveSystem : ISaveSystem
    {
        private GameData _gameData;
        private IInventoryService _inventory;

        public YandexCloudSaveSystem(IInventoryService inventory)
        {
            _inventory = inventory;
        }

        public GameData LoadGame()
        {
            _gameData = new GameData();

            _gameData.SetIsBossFailed(YG2.saves.IsBossFailed);
            _gameData.SetLevel(YG2.saves.Level);
            Debug.Log($"load level {YG2.saves.Level}");
            _gameData.AddCurrency(YG2.saves.Currency);


            if (YG2.saves.Module0Lvl == 0)
                _gameData.Module0Lvl = 1; 
            else
                _gameData.Module0Lvl = YG2.saves.Module0Lvl;

            _gameData.Module1Lvl = YG2.saves.Module1Lvl;
            _gameData.Module2Lvl = YG2.saves.Module2Lvl;

            _gameData.DroneIsReady = _gameData.Module1Lvl > 0;

            _gameData.Module0Rarity = (YG2.saves.Module0Rarity != null && YG2.saves.Module0Rarity != "") ? Enum.Parse<ItemRarity>(YG2.saves.Module0Rarity) : ItemRarity.COMMON;
            _gameData.Module1Rarity = (YG2.saves.Module1Rarity != null && YG2.saves.Module1Rarity != "") ? Enum.Parse<ItemRarity>(YG2.saves.Module1Rarity) : ItemRarity.COMMON;
            _gameData.Module2Rarity = (YG2.saves.Module1Rarity != null && YG2.saves.Module2Rarity != "") ? Enum.Parse<ItemRarity>(YG2.saves.Module2Rarity) : ItemRarity.COMMON;

            //TODO Load inventory
            if (YG2.saves.Inventory != null)
            {
                var size = _inventory.GetInventory("Player").Size;
                for (var x = 0; x < size.x; x++)
                {
                    for (var y = 0; y < size.y; y++)
                    {
                        IReadOnlyInventorySlot slot = YG2.saves.Inventory[x, y];
                        string itemId = slot.ItemId;
                        if (itemId != null)
                            _inventory.AddItems("Player", itemId, slot.Amount);
                    }
                }
            }

            _gameData.Settings = YG2.saves.Settings;

            if (YG2.saves.LastPlayedTime == null || YG2.saves.LastPlayedTime == "") 
                _gameData.LastPlayedTime = DateTime.UtcNow;
            else
                _gameData.LastPlayedTime = DateTime.Parse(YG2.saves.LastPlayedTime);

            Debug.Log($"save: {_gameData}");
            if (_gameData == null) 
            {
                _gameData = new GameData();
            }
            return _gameData;
        }

        public void SaveGame()
        {
            Debug.Log("Start Saving the game");
            if (_gameData.Modules == null) return;

            Debug.Log("Saving not breaked");


            YG2.saves.IsBossFailed = _gameData.IsBossFailed;
            YG2.saves.Currency = _gameData.Currency;
            YG2.saves.Level = _gameData.Level;
            Debug.Log($"save level {YG2.saves.Level}");

            int i = 0;
            foreach (ActiveUpgrade upg in _gameData.Modules)
            {
                switch (i)
                {
                    case 0:
                        YG2.saves.Module0Lvl = upg.CurrentLevel;
                        YG2.saves.Module0Rarity = upg.GetModule().GetRarity().ToString();
                        break;
                    case 1:
                        YG2.saves.Module1Lvl = upg.CurrentLevel;
                        YG2.saves.Module1Rarity = upg.GetModule().GetRarity().ToString();
                        break;
                    case 2:
                        YG2.saves.Module2Lvl = upg.CurrentLevel;
                        YG2.saves.Module2Rarity = upg.GetModule().GetRarity().ToString();
                        break;
                }

                i++;
            }
            //TODO Save inventory
            IReadOnlyInventorySlot[,] slots = _inventory.GetInventory("Player").GetSlots();
            YG2.saves.Inventory = slots;

            YG2.saves.LastPlayedTime = DateTime.UtcNow.ToString(); 

            YG2.saves.Settings = _gameData.Settings;

            YG2.SaveProgress();
        }

        public void SaveSettings(SettingsData data)
        {
            _gameData.Settings = data;
            SaveGame();
        }

        private void Load()
        {
            _gameData = LoadGame();
            YG2.onGetSDKData -= Load;
        }
    }
}
