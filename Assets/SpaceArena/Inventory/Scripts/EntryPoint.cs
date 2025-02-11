using Services;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class EntryPoint: MonoBehaviour
    {
        [SerializeField] private ScreenView _screenView;

        private const string OWNER_1 = "Player";
        //private readonly string[] _itemIds = { "TestGun", "TestDrone", "TestEngine", "TestTouret" };

        private IInventoryService _inventoryService;
        private ScreenController _screenController;


        private void Start()
        {
            _inventoryService = AllServices.Container.Single<IInventoryService>();

            _screenController = new ScreenController(_inventoryService, _screenView);
            _screenController.OpenInventory(OWNER_1);
            _screenView.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _screenController.OpenInventory(OWNER_1);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _screenView.gameObject.SetActive(!_screenView.gameObject.activeInHierarchy);
            }
        }

        public void OpenInventory()
        {
            Sound.instance.PlayButtonClick();
            _screenView.gameObject.SetActive(true);
            Debug.Log("Inventory is opened");
        }
    }
}
