using DamageNumbersPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyHandler : MonoBehaviour
{
    [SerializeField] 
    private Text currencyText;
    [SerializeField]
    private DamageNumber currencyNumbersPrefab;

    [SerializeField]
    public float _baseReward, _rewardGrowthRate, bossRewardMultiplier;


    private GameData _gameData;
    private float _currencyNumberOffset = 5f;

    public static Action<float> OnCurrencyChanged;

    private void OnEnable()
    {
        ClickerBootstrapper.OnGameLoaded += Init;
        EnemyHandler.OnEnemyKilled += AddReward;
        Modules.OnModuleUpgraded += RefreshCurrencyText;
        OfflineReward.OnRewardedCurrencyGetted += AddCurrency;
    }

    private void OnDisable()
    {
        ClickerBootstrapper.OnGameLoaded -= Init; 
        EnemyHandler.OnEnemyKilled -= AddReward;
        Modules.OnModuleUpgraded -= RefreshCurrencyText;
        OfflineReward.OnRewardedCurrencyGetted -= AddCurrency;
    }

    private void AddCurrency(float val)
    {
        _gameData.AddCurrency(val);
        currencyNumbersPrefab.Spawn(
            Vector3.zero + new Vector3(UnityEngine.Random.Range(0f, _currencyNumberOffset),
            0,
            UnityEngine.Random.Range(0f, _currencyNumberOffset)),
            $"+ ${ShortScaleString.parseDouble(val, 3, 1000, true)}");
        RefreshCurrencyText();
        OnCurrencyChanged?.Invoke(val);
    }

    private void Init(GameData gameData)
    {
        Debug.Log("Currency handler init");
        _gameData = gameData;
        RefreshCurrencyText();
    }

    private void RefreshCurrencyText()
    {
        currencyText.text = $"${ShortScaleString.parseDouble(_gameData.Currency, 3, 1000, true)}";
    }

    private void AddReward(EnemyData _enemy, float  coeff = 1)
    {
        float calculatedReward = Mathf.Ceil(_baseReward * Mathf.Pow(1 + _rewardGrowthRate, _enemy.EnemyLevel - 1)) * coeff;
        if (_enemy.IsBoss)
        {
            calculatedReward *= bossRewardMultiplier;
        }

        AddCurrency(calculatedReward);
    }
}
