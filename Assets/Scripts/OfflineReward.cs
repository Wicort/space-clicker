using Items;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineReward : MonoBehaviour
{
    private GameData _gameData;

    public Canvas OfflineRevardHUD;
    public Text CurrencyX2Text;
    public List<Item> RewardedItemsList = new();
    public float RewardedCurrency;
    public GameObject container;
    public GameObject prefab;

    public int TimeSpanRestriction = 2 * 60 * 60;

    public static Action<float> OnRewardedCurrencyGetted;
    public static Action<Item> OnRewardedItemGetted;

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
        RewardedCurrency = val;
        CurrencyHandler.OnCurrencyChanged -= SetReward;
        CurrencyX2Text.text = $"+ ${ShortScaleString.parseDouble(val, 3, 1000, true)}";
    }

    private void SetItemReward(Item item)
    {
        Debug.Log(item.Name);
        RewardedItemsList.Add(item);
        var reward = Instantiate(prefab, container.transform);
        reward.GetComponent<ItemLine>().Init(item);

    }

    public float Init(GameData gameData, float damage, float enemyHealth)
    {
        _gameData = gameData;
        var lastTime = _gameData.LastPlayedTime;
        var currentTime = DateTime.UtcNow;

        if (lastTime == null) return 0;

        
        float delta = (float)Math.Clamp((currentTime - lastTime).TotalSeconds, 0, TimeSpanRestriction);
        Debug.Log($"TimeSpanRestriction: {TimeSpanRestriction}");
        Debug.Log($"delta: {delta}");
        Debug.Log($"currentTime - lastTime: {(currentTime - lastTime).TotalSeconds}");
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
        DropHandler.OnItemDropped -= SetItemReward;
        Debug.Log("X2 getted");
        OnRewardedCurrencyGetted?.Invoke(RewardedCurrency);
        
        foreach (Item item in RewardedItemsList)
        {
            OnRewardedItemGetted?.Invoke(item);
        }

        OfflineRevardHUD.gameObject.SetActive(false);
        StartCoroutine(SaveAndClose());

        
    }

    System.Collections.IEnumerator SaveAndClose()
    {
        yield return new WaitForSeconds(2f);
        ISaveSystem saveSystem = AllServices.Container.Single<ISaveSystem>();
        saveSystem.Save(_gameData);
        OnCloseBullonClick();
    }
    
}
