using System;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Text _name;
        [SerializeField]
        private Text _description;
        [SerializeField]
        private Text _price;
        [SerializeField]
        private Text _level;
        [SerializeField]
        private Button _upgradeButton;

        public int Id => _id;


        public static Action<int> OnModuleUpgraded;

        public void Init(int id,
            Sprite icon,
            string name,
            string description,
            float price,
            int level,
            bool isBtnEnabled)
        {
            _id = id;
            _icon.sprite = icon;
            _name.text = name;
            _description.text = description;
            UpdateInfo(icon, name, description, price, level, isBtnEnabled);
        }

        public void OnUpgradeButtonClick()
        {
            if (_id == 0) return;

            OnModuleUpgraded?.Invoke(_id);
        }

        public void SetInterractable(bool isBtnEnabled)
        {
            _upgradeButton.interactable = isBtnEnabled;
        }

        public void UpdateInfo(Sprite icon, string name, string description, float price, int level, bool isBtnEnabled)
        {
            _icon.sprite = icon;
            _name.text = name;
            _description.text = description;
            _price.text = $"${ShortScaleString.parseDouble(price,1, 1000, true)}";
            _level.text = $"Lvl. {level}";
            SetInterractable(isBtnEnabled);
        }
    }
}