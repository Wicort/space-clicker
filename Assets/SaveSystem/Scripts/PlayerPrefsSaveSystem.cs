
using Items;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.SaveSystem.Scripts
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string IS_BOSS_FAILED = "IsBossFailed";
        private const string LEVEL = "Level";
        private const string CURRENCY = "Currency";

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

            return gameData;

        }

        public void Save(GameData data)
        {
            PlayerPrefs.SetInt(LEVEL, data.Level);
            PlayerPrefs.SetInt(IS_BOSS_FAILED, data.IsBossFailed ? 1 : 0);
            PlayerPrefs.SetFloat(CURRENCY, data.Currency);

            if (data.Modules == null) return;

            int i = 0;
            foreach(ActiveUpgrade upg in data.Modules)
            {
                PlayerPrefs.SetInt("Module" + i + "Lvl", upg.CurrentLevel);
                i++;
            }
        }

        private string LoadString(string key)
        {
            if (key == null) return null;

            if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetString(key);

            return null;
        }

        private int LoadInt(string key)
        {
            if (key == null) return 0;

            if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetInt(key);

            return 0;
        }

        private float LoadFloat(string key)
        {
            if (key == null) return 0f;

            if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetFloat(key);

            return 0f;
        }
    }
}
