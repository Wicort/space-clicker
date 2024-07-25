namespace Inventory
{
    public class ScreenController
    {
        private readonly InventoryService _inventoryService;
        private readonly ScreenView _view;

        private InventoryGridController _currentInventoryController;

        public ScreenController(InventoryService inventoryService, ScreenView view)
        {
            _inventoryService = inventoryService;
            _view = view;
        }

        public void OpenInventory(string OownerId)
        {
            var inventory = _inventoryService.GetInventory(OownerId);
            var inventoryView = _view.InventoryView;

            _currentInventoryController = new InventoryGridController(inventory, inventoryView);
        }
    }
}
