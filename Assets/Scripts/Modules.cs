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
        GameBootstrapper.OnGameLoaded += Init;
        UpgradeButton.OnModuleUpgraded += UpgradeModule;
        CurrencyHandler.OnCurrencyChanged += RefreshUpgradeButtons;
        ActionPanel.OnEquipButtonClicked += EquipItem;
    }

    private void OnDisable()
    {
        GameBootstrapper.OnGameLoaded -= Init;
        UpgradeButton.OnModuleUpgraded -= UpgradeModule;
        CurrencyHandler.OnCurrencyChanged -= RefreshUpgradeButtons;
        ActionPanel.OnEquipButtonClicked -= EquipItem;
    }

    private void EquipItem(string itemId)
    {
        Item item = AllServices.Container.Single<IItemService>().GetItemInfo(itemId);

        ActiveUpgrade upgrade = _gameData.Modules.Find(upg => upg.GetModule().GetItemType() == item.ItemType);
        Debug.Log($"upgrade {upgrade.GetModule().GetName()}!");

        upgrade.GetModule().SetItem(item);

        Debug.Log($"upgrade {upgrade.GetModule().GetName()}!");
        upgrade = _gameData.Modules.Find(upg => upg.GetModule().GetItemType() == item.ItemType);
        Debug.Log($"upgrade {upgrade.GetModule().GetName()}!");

        RefreshUpgradeButtons();
        OnModuleUpgraded?.Invoke();

        Debug.Log($"Equiped item {itemId}!");
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

    private void RefreshUpgradeButtons()
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
        foreach (var upgrade in _upgrades)
        {
            Item item = _itemService.GetCommonItemByType(upgrade.GetModule().ModuleItemType);
            if (item == null) continue;
            upgrade.GetModule().SetItem(item);

            if (upgrade.CurrentLevel != 0)
            {
                
                upgrade.SetCurrentPrice(CalculateModulePrice(upgrade.CurrentLevel, upgrade.GetModule().StartPrice));
            }
            _gameData.Modules.Add(upgrade);
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
