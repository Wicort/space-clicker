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

            _view.Title = slot.ItemId;
            _view.Amount = slot.Amount;
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
