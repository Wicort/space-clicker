using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class ActiveUpgrade
    {
        [SerializeField]
        private Module _module;
        [SerializeField]
        private int _currentLevel;
        [SerializeField]
        private float _currentPrice;

        public Module GetModule() { return _module; }
        public int CurrentLevel => _currentLevel;
        public float CurrentPrice => _currentPrice;

        public static Action OnFirstDroneUpgrade;

        public void SetCurrentPrice(float value)
        {
            _currentPrice = value;
        }

        public void UpgradeLevel(int count = 1)
        {
            if (_module.ModuleItemType == ItemType.DRONE && _currentLevel == 0 && count > 0)
                OnFirstDroneUpgrade?.Invoke();

            _currentLevel += count;
        }

        public void SetLevel(int value)
        {
            _currentLevel = value;
        }
    }
}