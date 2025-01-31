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

        public void SetCurrentPrice(float value)
        {
            _currentPrice = value;
        }

        public void UpgradeLevel(int count = 1)
        {
            _currentLevel += count;
        }

        public void SetLevel(int value)
        {
            _currentLevel = value;
        }
    }
}