using Assets.Services;
using Items;
using Services;
using UnityEngine;

namespace Inventory
{
    public class InventorySlotController
    {
        private readonly InventorySlotView _view;

        public InventorySlotController(
            IReadOnlyInventorySlot slot, 
            InventorySlotView view)
        {
            _view = view;

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;
            slot.ItemSpriteChanged += OnSlotItemSpriteChanged;

            Item item = AllServices.Container.Single<IItemService>().GetItemInfo(slot.ItemId);
            if (item != null)
            {
                _view.Title = item.Description;
                _view.Amount = slot.Amount;
                _view.ItemSprite = item.Icon;
                _view.ItemId = item.Id;
            }
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            _view.Amount = newAmount;
        }

        private void OnSlotItemIdChanged(string newItemId)
        {
            Item item = AllServices.Container.Single<IItemService>().GetItemInfo(newItemId);
            _view.Title = item.Name;
            _view.ItemId = newItemId;
        }

        private void OnSlotItemSpriteChanged(Sprite newSprite)
        {
            _view.ItemSprite = newSprite;
        }
    }
}
