using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotView: MonoBehaviour
    {
        [SerializeField] private Text _textTitle;
        [SerializeField] private Text _textAmount;
        [SerializeField] private Image _itemImage;

        public static Action<string, int> OnInventoryButtonClicked;

        private string _itemId;

        private void Awake()
        {
            _textTitle.text = "";
            _textAmount.text = "";
            _itemId = "";
        }

        public string Title
        {
            get => _textTitle.text;
            set => _textTitle.text = value;
        }

        public int Amount
        {
            get => Convert.ToInt32(Amount);
            set => _textAmount.text = value == 0 ? "" : value.ToString();
        }

        public Sprite ItemSprite
        {
            get => _itemImage.sprite;
            set
            {
                _itemImage.sprite = value;
            }
        }

        public string ItemId
        {
            get => _itemId;
            set 
            { 
                _itemId = value;
            }
        }

        public void OnInventoryButtonClick()
        {
            if (_itemId != null) OnInventoryButtonClicked?.Invoke(_itemId, Convert.ToInt32(_textAmount.text));
        }
    }
}
