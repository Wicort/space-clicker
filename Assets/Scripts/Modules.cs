using Assets.Services;
using Inventory;
using Items;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Modules : MonoBehaviour
{
    [SerializeField]
    private List<ActiveUpgrade> _upgrades;
    [SerializeField]
    private float _priceGrowthRate = 0.2f;
    [SerializeField]
    private UpgradeButton _upgradeButtonPrefab;
    [SerializeField]
    private GameObject _content;

    private GameData _gameData;
    private List<UpgradeButton> _buttons;
    private ItemService _itemService;

    public static Action OnModuleUpgraded;

    private void OnEnable()
    {
        ClickerBootstrapper.OnGameLoaded += Init;
        UpgradeButton.OnModuleUpgraded += UpgradeModule;
        CurrencyHandler.OnCurrencyChanged += RefreshUpgradeButtons;
        ActionPanel.OnEquipButtonClicked += EquipItem;
        ActionPanel.OnUseUpgradeButtonClicked += UseUpgradeModule;
    }

    private void OnDisable()
    {
        ClickerBootstrapper.OnGameLoaded -= Init;
        UpgradeButton.OnModuleUpgraded -= UpgradeModule;
        CurrencyHandler.OnCurrencyChanged -= RefreshUpgradeButtons;
        ActionPanel.OnEquipButtonClicked -= EquipItem;
        ActionPanel.OnUseUpgradeButtonClicked -= UseUpgradeModule;
    }

    private void EquipItem(string itemId)
    {
        Item item = AllServices.Container.Single<IItemService>().GetItemInfo(itemId);

        ActiveUpgrade upgrade = _gameData.Modules.Find(upg => upg.GetModule().GetItemType() == item.ItemType);
        string oldItemId = upgrade.GetModule().GetId();
        upgrade.GetModule().SetItem(item);
        upgrade = _gameData.Modules.Find(upg => upg.GetModule().GetItemType() == item.ItemType);

        OnModuleUpgraded?.Invoke();
        RefreshUpgradeButtons();

        Debug.Log($"Equiped item {itemId}!");
        IInventoryService inventoryService = AllServices.Container.Single<IInventoryService>();
        inventoryService.RemoveItems("Player", itemId, 1);
        inventoryService.AddItems("Player", oldItemId, 1);
    }

    private void Init(GameData gameData)
    {
        _gameData = gameData;
        _itemService = new ItemService();


        if (_gameData.Modules == null || _gameData.Modules.Count == 0)
        {
            FirstInitialization();
        }

        _buttons = new List<UpgradeButton>();

        foreach (ActiveUpgrade mData in _gameData.Modules)
        {
            ActiveUpgrade upgradeBtn = _upgrades.Find(upg => upg.GetModule().Id == mData.GetModule().Id);
            upgradeBtn.SetCurrentPrice(CalculateModulePrice(upgradeBtn.CurrentLevel, upgradeBtn.GetModule().StartPrice));
            
            Module module = upgradeBtn.GetModule();
            bool isModuleBtnEnabled = IsModuleButtonActive(upgradeBtn);

            UpgradeButton btn = Instantiate(_upgradeButtonPrefab, _content.transform);
            btn.Init(module.Id, module.GetIcon(), module.GetName(), module.GetDescription(), upgradeBtn.CurrentPrice, upgradeBtn.CurrentLevel, isModuleBtnEnabled);

            _buttons.Add(btn);
        }
    }

    private void RefreshUpgradeButtons(float val = 0)
    {
        foreach (UpgradeButton btn in _buttons)
        {
            ActiveUpgrade upgrade = _upgrades.Find(upg => upg.GetModule().Id == btn.Id);
            btn.UpdateInfo(
                upgrade.GetModule().GetIcon(), 
                upgrade.GetModule().GetName(),
                upgrade.GetModule().GetDescription(),
                upgrade.CurrentPrice, 
                upgrade.CurrentLevel, 
                IsModuleButtonActive(upgrade));
        }
    }

    private void FirstInitialization()
    {
        _gameData.Modules = new List<ActiveUpgrade>();
        int i = 0;
        foreach (var upgrade in _upgrades)
        {
            //Item item = _itemService.GetCommonItemByType(upgrade.GetModule().ModuleItemType);
            Item item = _itemService.GetItemByTypeAndRariry(upgrade.GetModule().ModuleItemType,
                i == 0 ? _gameData.Module0Rarity : (i == 1 ? _gameData.Module1Rarity : _gameData.Module2Rarity));
            if (item == null) continue;
            upgrade.GetModule().SetItem(item);

            upgrade.SetLevel(i == 0 ? _gameData.Module0Lvl : (i == 1 ? _gameData.Module1Lvl : _gameData.Module2Lvl));

            if (upgrade.CurrentLevel != 0)
            {
                upgrade.SetCurrentPrice(CalculateModulePrice(upgrade.CurrentLevel, upgrade.GetModule().StartPrice));
            }
            

            _gameData.Modules.Add(upgrade);
            i++;
        }
    }

    private float CalculateModulePrice(int level, float startPrice)
    {
        if (level > 0)
            return Mathf.Ceil(startPrice * Mathf.Pow(1 + _priceGrowthRate, level));
        else return startPrice;
    }

    public void UpgradeModule(int id)
    {
        var upgradeBtn = _upgrades.Find(upg => upg.GetModule().Id == id);

        _gameData.AddCurrency(-1 * upgradeBtn.CurrentPrice);
        
        upgradeBtn.UpgradeLevel();
        upgradeBtn.SetCurrentPrice(CalculateModulePrice(upgradeBtn.CurrentLevel, upgradeBtn.GetModule().StartPrice));

        OnModuleUpgraded?.Invoke();

        RefreshUpgradeButtons();
    }

    private void UseUpgradeModule(string itemId, int count = 1)
    {
        Item item = _itemService.GetItemInfo(itemId);
        var upgradeBtn = _upgrades.Find(upg => upg.GetModule().GetItemType() == item.ItemType);

        /*var requiredUpgrade = upgradeBtn.GetModule().RequiredUpgrade;
        var requiredLevel = upgradeBtn.GetModule().RequiredLevel;
        var requiredItemType = requiredUpgrade.GetItemType();
        var rquiredModuleLevel = _upgrades.Find(upg => upg.GetModule().GetItemType() == requiredItemType).CurrentLevel;*/

        int rarity = Convert.ToInt32(item.Rarity) + 1;
        Debug.Log($"upgrading item {itemId}, rarity {rarity}");

        int upgradeValue = count * rarity;

        for (int i = 0; i < upgradeValue; i++)
        {
            upgradeBtn.UpgradeLevel();
        }        
        upgradeBtn.SetCurrentPrice(CalculateModulePrice(upgradeBtn.CurrentLevel, upgradeBtn.GetModule().StartPrice));

        IInventoryService inventoryService = AllServices.Container.Single<IInventoryService>();
        inventoryService.RemoveItems("Player", itemId, count);

        OnModuleUpgraded?.Invoke();

        RefreshUpgradeButtons();
    }

    private bool IsModuleButtonActive(ActiveUpgrade upgradeBtn)
    {
        bool isButtonEnabled = false;
        Module module = upgradeBtn.GetModule();

        if (upgradeBtn.CurrentPrice > _gameData.Currency) return false;

        if (module.RequiredUpgrade == null || module.RequiredLevel == 0)
        {
            isButtonEnabled = true;
        } else if (module.RequiredUpgrade != null)
        {
            Module requiredModule = module.RequiredUpgrade;
            ActiveUpgrade requiredUpdate = _upgrades.Find(upg => upg.GetModule().Id == requiredModule.Id);
            if (requiredUpdate.CurrentLevel >= module.RequiredLevel) { 
                isButtonEnabled = true; 
            }
        }

        return isButtonEnabled;
    }        
}
