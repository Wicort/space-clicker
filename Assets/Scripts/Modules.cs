using System.Collections.Generic;
using UnityEngine;

public class Modules : MonoBehaviour
{
    [SerializeField]
    private List<ActiveModule> modules;
    [SerializeField]
    private float _priceGrowthRate = 0.2f;
    [SerializeField]
    private ModuleButton _moduleButtonPrefab;
    [SerializeField]
    private GameObject _content;

    private GameData _gameData;

    private void OnEnable()
    {
        GameBootstrapper.OnGameLoaded += Init;
    }

    private void OnDisable()
    {
        GameBootstrapper.OnGameLoaded -= Init;
    }

    private void Init(GameData gameData)
    {
        _gameData = gameData;

        
        if (_gameData.Modules == null || _gameData.Modules.Count == 0)
        {
            FirstInitialization();
        }

        foreach (ActiveModule mData in _gameData.Modules)
        {
            var module = modules.Find(m => m.GetModule().Id == mData.GetModule().Id);
            module.SetCurrentPrice(CalculateModulePrice(module.CurrentLevel, module.GetModule().StartPrice));
            Debug.Log(module.GetModule().name);
            ModuleButton btn = Instantiate(_moduleButtonPrefab, _content.transform);
            btn.Init(module.GetModule().Icon, module.GetModule().Name, module.GetModule().Description, $"${module.CurrentPrice}");
        }
    }

    private void FirstInitialization()
    {
        _gameData.Modules = new List<ActiveModule>();
        foreach (var module in modules)
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
}
