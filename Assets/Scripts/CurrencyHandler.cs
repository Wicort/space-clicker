using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyHandler : MonoBehaviour
{
    [SerializeField] 
    private Text currencyText;

    [SerializeField]
    public float _baseReward, _rewardGrowthRate, bossRewardMultiplier;

    private GameData _gameData;

    private void OnEnable()
    {
        GameBootstrapper.OnGameLoaded += Init;
        EnemyHandler.OnEnemyKilled += AddReward;
    }

    private void OnDisable()
    {
        GameBootstrapper.OnGameLoaded -= Init; 
        EnemyHandler.OnEnemyKilled -= AddReward;
    }

    private void Init(GameData gameData)
    {
        _gameData = gameData;
        RefreshCurrencyText();
    }

    private void RefreshCurrencyText()
    {
        currencyText.text = $"${_gameData.Currency}";
    }

    private void AddReward(EnemyData _enemy)
    {
        float calculatedReward = Mathf.Ceil(_baseReward * Mathf.Pow(1 + _rewardGrowthRate, _enemy.EnemyLevel - 1));
        if (_enemy.IsBoss)
        {
            calculatedReward *= bossRewardMultiplier;
        }

        _gameData.Currency += calculatedReward;
        RefreshCurrencyText();
    }
}
