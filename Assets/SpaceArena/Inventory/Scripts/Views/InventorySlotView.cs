using Assets.Services;
using Services;
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
        [SerializeField] private Sprite _emptySprite;

        public static Action<string, int> OnInventoryButtonClicked;

        private string _itemId;
        private IItemService _itemService;

        private void Awake()
        {
            _textTitle.text = "";
            _textAmount.text = "";
            _itemId = "";
            _itemService = AllServices.Container.Single<IItemService>();
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
                Debug.Log(value);
                if (_itemId != null && _itemId != "")
                    _itemImage.sprite = value;
                else _itemImage.sprite = _emptySprite;
            }
        }

        public string ItemId
        {
            get => _itemId;
            set 
            { 
                _itemId = value;
                Debug.Log(_itemId);
                if (_itemId != null && _itemId != "")
                    _itemImage.sprite = _itemService.GetItemInfo(value).Icon;
                else _itemImage.sprite = _emptySprite;
            }
        }

        public void OnInventoryButtonClick()
        {
            if (_itemId != null && _itemId != "") 
                OnInventoryButtonClicked?.Invoke(_itemId, Convert.ToInt32(_textAmount.text));
        }
    }
}
