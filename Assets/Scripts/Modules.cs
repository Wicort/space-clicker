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

    private void OnEnable()
    {
        GameBootstrapper.OnGameLoaded += Init;
        UpgradeButton.OnModuleUpgraded += UpgradeModule;
    }

    private void OnDisable()
    {
        GameBootstrapper.OnGameLoaded -= Init;
        UpgradeButton.OnModuleUpgraded -= UpgradeModule;
    }

    private void Init(GameData gameData)
    {
        _gameData = gameData;


        if (_gameData.Modules == null || _gameData.Modules.Count == 0)
        {
            FirstInitialization();
        }

        _buttons = new List<UpgradeButton>();

        foreach (ActiveUpgrade mData in _gameData.Modules)
        {
            ActiveUpgrade upgradeBtn = _upgrades.Find(upg => upg.GetModule().Id == mData.GetModule().Id);
            upgradeBtn.SetCurrentPrice(CalculateModulePrice(upgradeBtn.CurrentLevel, upgradeBtn.GetModule().StartPrice));
            Debug.Log(upgradeBtn.GetModule().name);

            Module module = upgradeBtn.GetModule();
            bool isModuleBtnEnabled = IsModuleButtonActive(upgradeBtn);

            UpgradeButton btn = Instantiate(_upgradeButtonPrefab, _content.transform);
            btn.Init(module.Id, module.Icon, module.Name, module.Description, $"${upgradeBtn.CurrentPrice}", isModuleBtnEnabled);

            _buttons.Add(btn);
        }
    }

    private void RefreshUpgradeButtons()
    {
        foreach (UpgradeButton btn in _buttons)
        {
            btn.SetInterractable(IsModuleButtonActive(_upgrades.Find(upg => upg.GetModule().Id == btn.Id)));
        }
    }

    private void FirstInitialization()
    {
        _gameData.Modules = new List<ActiveUpgrade>();
        foreach (var module in _upgrades)
        {
            if (module.CurrentLevel != 0)
            {
                module.SetCurrentPrice(CalculateModulePrice(module.CurrentLevel, module.GetModule().StartPrice));
            }
            _gameData.Modules.Add(module);
        }
    }

    private float CalculateModulePrice(int level, float startPrice)
    {
        if (level > 0)
            return startPrice * Mathf.Pow(1 + _priceGrowthRate, level - 1);
        else return startPrice;
    }

    public void UpgradeModule(int id)
    {
        Debug.Log($"{id}");

        var upgradeBtn = _upgrades.Find(upg => upg.GetModule().Id == id);

        // if has a money
        upgradeBtn.UpgradeLevel();
        upgradeBtn.SetCurrentPrice(CalculateModulePrice(upgradeBtn.CurrentLevel, upgradeBtn.GetModule().StartPrice));

        RefreshUpgradeButtons();
        
    }

    private bool IsModuleButtonActive(ActiveUpgrade upgradeBtn)
    {
        bool isButtonEnabled = false;
        Module module = upgradeBtn.GetModule();

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
