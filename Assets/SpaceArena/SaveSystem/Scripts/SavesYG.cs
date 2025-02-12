using Items;
using System.Collections.Generic;
using System;
using Assets.SpaceArena.SaveSystem.Scripts;
using Inventory;

namespace YG
{
    public partial class SavesYG
    {
        public bool IsBossFailed;
        public int Level;
        public float Currency;

        public int Module0Lvl;
        public string Module0Rarity;
        public int Module1Lvl;
        public string Module1Rarity;
        public int Module2Lvl;
        public string Module2Rarity;

        public string LastPlayedTime;

        public SettingsData Settings;

        public IReadOnlyInventorySlot[,] Inventory;
    }
}
