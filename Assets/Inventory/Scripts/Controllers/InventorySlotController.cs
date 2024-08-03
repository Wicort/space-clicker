using Items;
using System.Diagnostics;

namespace Inventory
{
    public class InventorySlotController
    {
        private readonly InventorySlotView _view;
        private ItemService _itemService = new ItemService();

        public InventorySlotController(
            IReadOnlyInventorySlot slot, 
            InventorySlotView view)
        {
            _view = view;

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemIdChanged;

            Item item = _itemService.GetItemInfo(slot.ItemId);
            if (item != null)
            { 
                _view.Title = item.Description;
                _view.Amount = slot.Amount;
            }
        }

        private void OnSlotItemIdChanged(int newAmount)
        {
            _view.Amount = newAmount;
        }

        private void OnSlotItemIdChanged(string newItemId)
        {
            _view.Title = newItemId;
        }
    }
}
