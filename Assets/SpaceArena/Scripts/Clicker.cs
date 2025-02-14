using Assets.Services;
using Items;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [SerializeField]
    private EnemyHandler _enemyHandler;
    [SerializeField]
    private OfflineReward _offlineReward;
    [SerializeField] private GameObject _lbScreen;
    [SerializeField] private GameObject _settingsScreen;

    private GameData _gameData;
    private float _clickDamage = 0;
    private float _autoClickDamage = 0;

    private float _clickMultiplier = 1f;
    private float _autoClickMultiplier = 1f;

    public static Action<float> OnEnemyAttacked;
    public static Action<Vector3> OnPlayerClick;
    public static Action<Vector3> OnAutoclick;
    public static Action OnArenaButtonClicked;
    public static Action OnMenuButtonClicked;

    private void OnEnable()
    {
        ClickerBootstrapper.OnGameLoaded += Init;
        Modules.OnModuleUpgraded += RecalcDamage;
        AdsService.OnStartDoubleDamage += SetDoubleDamage;
        AdsService.OnStopDoubleDamage += SetSingleDamage;
    }

    private void OnDisable()
    {
        ClickerBootstrapper.OnGameLoaded -= Init;
        Modules.OnModuleUpgraded -= RecalcDamage;
        AdsService.OnStartDoubleDamage -= SetDoubleDamage;
        AdsService.OnStopDoubleDamage -= SetSingleDamage;
    }

    private void Start()
    {
        _lbScreen.SetActive(false);
        _settingsScreen.SetActive(false);

        StartCoroutine(Autoclick());
    }

    public void Init(GameData gameData)
    {
        _gameData = gameData;
        RecalcDamage();
        if (_enemyHandler.GetCurrentEnemyLevel() > 1)
        {
            _enemyHandler.OfflineKilling(_offlineReward.Init(_gameData, GetAllDamageInSecond(), _enemyHandler.GetCurrentEnemyHealth()));
            _offlineReward.ShowOfflineRewardPanel();
        }
        
    }

    public void OnClick()
    {
        OnEnemyAttacked?.Invoke(GetClickDamage());
        OnPlayerClick?.Invoke(_enemyHandler.GetEnemyPosition());
        Sound.instance.PlayClickShot();
    }

    public void OnAutoClick()
    {
        OnEnemyAttacked?.Invoke(GetAutoClickDamage());
        OnAutoclick?.Invoke(_enemyHandler.GetEnemyPosition());
    }

    public float GetAllDamageInSecond()
    {
        return _autoClickDamage + _clickDamage;
    }

    public void OnArenaButtonClick()
    {
        OnArenaButtonClicked?.Invoke();
    }
    public void OnMenuButtonClick()
    {
        OnMenuButtonClicked?.Invoke();
    }
    public void OnLeaderBoardButtonClick()
    {
        _lbScreen.SetActive(true);
    }
    
    public void OnSettingsButtonClick()
    {
        _settingsScreen.SetActive(true);
    }

    private void SetDoubleDamage()
    {
        _clickMultiplier = 2f;
        _autoClickMultiplier = 2f;
    }

    private void SetSingleDamage()
    {
        _clickMultiplier = 1f;
        _autoClickMultiplier = 1f;
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
        return _clickDamage * _clickMultiplier;
    }

    private float GetAutoClickDamage()
    {
        return _autoClickDamage * _autoClickMultiplier;
    }

    private void RecalcDamage()
    {
        System.Collections.Generic.List<ActiveUpgrade> autoClickModules = _gameData.Modules.FindAll(u => u.GetModule().ModuleAttackType == AttackType.CLICK || u.CurrentLevel > 0);
        var res = from module in autoClickModules
                  group module by module.GetModule().ModuleAttackType 
                  into groupDmg
                  select new { Id = groupDmg.Key, Dmg = groupDmg.Sum(module => module.CurrentLevel * module.GetModule().StartValue + Mathf.Pow(1 + module.GetModule().ValueGrowthRate, module.CurrentLevel - 1)) };
        

        foreach (var result in res)
        {
            if (result.Id == AttackType.CLICK)
            {
                _clickDamage = result.Dmg;
            } else if (result.Id == AttackType.AUTOCLICK)
            {
                _autoClickDamage = result.Dmg;
            }
        }
        //_clickDamage = 500000f;
    }
}
