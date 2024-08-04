using Items;
using Services;

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
            slot.ItemAmountChanged += OnSlotItemIdChanged;

            Item item = AllServices.Container.Single<IItemService>().GetItemInfo(slot.ItemId);
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
