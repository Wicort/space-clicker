using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public static Action<float> OnEnemyAttacked;

    private GameData _gameData;
    private float _clickDamage = 0;
    private float _autoClickDamage = 0;

    private void OnEnable()
    {
        GameBootstrapper.OnGameLoaded += Init;
        Modules.OnModuleUpgraded += RecalcDamage;
    }

    private void OnDisable()
    {
        GameBootstrapper.OnGameLoaded -= Init;
        Modules.OnModuleUpgraded -= RecalcDamage;
    }

    private void Init(GameData gameData)
    {
        _gameData = gameData;
        RecalcDamage();
    }

    private void Start()
    {
        StartCoroutine(Autoclick());
    }

    public void OnClick()
    {
        OnEnemyAttacked?.Invoke(GetClickDamage());
    }

    public void OnAutoClick()
    {
        OnEnemyAttacked?.Invoke(GetAutoClickDamage());
    }

    private IEnumerator Autoclick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            OnAutoClick();
        }
    }

    private float GetClickDamage()
    {
        return _clickDamage;
    }

    private float GetAutoClickDamage()
    {
        return _autoClickDamage;
    }

    private void RecalcDamage()
    {
        System.Collections.Generic.List<ActiveUpgrade> autoClickModules = _gameData.Modules.FindAll(u => u.GetModule().Type == ModuleType.CLICK || u.CurrentLevel > 0);
        var res = from module in autoClickModules
                  group module by module.GetModule().Type 
                  into groupDmg
                  select new { Id = groupDmg.Key, Dmg = groupDmg.Sum(module => module.CurrentLevel * module.GetModule().StartValue + Mathf.Pow(1 + module.GetModule().ValueGrowthRate, module.CurrentLevel - 1)) };
        

        foreach (var result in res)
        {
            Debug.Log($"id: {result.Id}, dmg: {result.Dmg}");
            if (result.Id == ModuleType.CLICK)
            {
                _clickDamage = result.Dmg;
            } else if (result.Id == ModuleType.AUTOCLICK)
            {
                _autoClickDamage = result.Dmg;
            }
        }

    }
}
