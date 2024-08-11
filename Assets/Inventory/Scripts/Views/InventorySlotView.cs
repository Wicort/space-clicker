
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private TMP_Text _textAmount;
        [SerializeField] private Image _itemImage;

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
                Debug.Log($"sprite name {_itemImage.sprite.name}");
            }
        }
    }
}
