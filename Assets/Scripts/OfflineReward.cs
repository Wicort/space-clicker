using Items;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OfflineReward : MonoBehaviour
{
    private GameData _gameData;

    public Canvas OfflineRevardHUD;
    public Text CurrencyX2Text;

    public int TimeSpanRestriction = 2 * 60 * 60;

    private void OnEnable()
    {
        CurrencyHandler.OnCurrencyChanged += SetReward;
        DropHandler.OnItemDropped += SetItemReward;
    }

    private void OnDisable()
    {
        CurrencyHandler.OnCurrencyChanged -= SetReward;
        DropHandler.OnItemDropped -= SetItemReward;
    }

    private void Awake()
    {
        OfflineRevardHUD.gameObject.SetActive(false);
    }

    private void SetReward(float val)
    {
        CurrencyHandler.OnCurrencyChanged -= SetReward;
        CurrencyX2Text.text = $"+ ${ShortScaleString.parseDouble(val, 3, 1000, true)}";
    }

    private void SetItemReward(Item item)
    {
        DropHandler.OnItemDropped -= SetItemReward;
        Debug.Log(item.Name);
    }

    public float Init(GameData gameData, float damage, float enemyHealth)
    {
        _gameData = gameData;
        var lastTime = _gameData.LastPlayedTime;
        var currentTime = DateTime.UtcNow;

        if (lastTime == null) return 0;

        
        float delta = (float)Math.Clamp((currentTime - lastTime).TotalSeconds, 0, TimeSpanRestriction);
        var timeToKillEnemy = Mathf.Max(1f, enemyHealth / damage);
        var offlineEnemyKilled = delta / timeToKillEnemy;
        
        return offlineEnemyKilled / 2f;
    }

    public void OnCloseBullonClick()
    {
        gameObject.SetActive(false);
    }

    public void ShowOfflineRewardPanel()
    {
        OfflineRevardHUD.gameObject.SetActive(true);
        
    }

    public void GetX2()
    {
        Debug.Log("X2 getted");
        OnCloseBullonClick();
    }

    
}
