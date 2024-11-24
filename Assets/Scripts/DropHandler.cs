using Assets.Services;
using DamageNumbersPro;
using Inventory;
using Items;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandler : MonoBehaviour
{
    [SerializeField]
    private WavesHandler _wavesHandler;
    [SerializeField]
    private DamageNumber dropDNPrefab;

    private IItemService _itemService;
    private IInventoryService _inventoryService;

    public static Action<Item> OnItemDropped;

    private void OnEnable()
    {
        EnemyHandler.OnEnemyKilled += GetDrop;
        OfflineReward.OnRewardedItemGetted += GetDrop;
    }

    private void GetDrop(Item item)
    {
        _inventoryService.AddItems("Player", item.Id, 1);
        OnItemDropped?.Invoke(item);

        Color dropColor = Color.black;
        switch (item.Rarity)
        {
            case ItemRarity.COMMON: dropColor = Color.white; break;
            case ItemRarity.UNCOMMON: dropColor = Color.green; break;
            case ItemRarity.RARE: dropColor = Color.blue; break;
            case ItemRarity.EPIC: dropColor = Color.magenta; break;
            case ItemRarity.LEGENDARY: dropColor = Color.yellow; break;
            case ItemRarity.MYTHICAL: dropColor = Color.red; break;
        }

        var dropText = dropDNPrefab.Spawn(Vector3.zero +
            new Vector3(2, -4, 0),
            $"{item.Name} added");
        dropText.SetColor(dropColor);
        dropText.UpdateText();
    }

    private void OnDisable()
    {
        EnemyHandler.OnEnemyKilled -= GetDrop;
    }

    private void GetDrop(EnemyData enemy, float coeff = 1)
    {
        Wave currentWave = _wavesHandler.getWave(enemy.EnemyLevel);
        for (int i = 0; i < coeff; i++)
        {
            GetDrop(enemy.EnemyLevel, currentWave);
        }
    }

    public List<Item> GetDrop(int enemyLevel, Wave wave)
    {
        List<Item> drop = new List<Item>();

        _itemService = AllServices.Container.Single<IItemService>();
        _inventoryService = AllServices.Container.Single<IInventoryService>();

        bool isBoss = (enemyLevel > 0 && enemyLevel % 10 == 0);

        var dropChance = isBoss ? 1 : wave.DropChance;
        var dropCount = isBoss ? wave.BossDropCount : 1;

        for (int i = 0; i < dropCount; i++)
        {
            var currentChance = UnityEngine.Random.Range(0f, 1f);
            if (isBoss)
            {
                currentChance *= wave.BossDropMultiplier;
            }
            if (currentChance > 1) currentChance = 1;
            if (currentChance <= dropChance)
            {
                
                Item item = _itemService.GetRandomItemByRarity(CalcRarity(wave.RarityDropChance, isBoss ? wave.BossDropMultiplier : 1));
                GetDrop(item);
            }
        }

        return drop;
    }

    private ItemRarity CalcRarity(RarityDictionary dictionary, float multiplyer = 1)
    {
        var chance = UnityEngine.Random.Range(0f, 100f) * multiplyer;
        
        if (checkRarity(dictionary, ItemRarity.MYTHICAL, chance)) return ItemRarity.MYTHICAL;
        if (checkRarity(dictionary, ItemRarity.LEGENDARY, chance)) return ItemRarity.LEGENDARY;
        if (checkRarity(dictionary, ItemRarity.EPIC, chance)) return ItemRarity.EPIC;
        if (checkRarity(dictionary, ItemRarity.RARE, chance)) return ItemRarity.RARE;
        if (checkRarity(dictionary, ItemRarity.UNCOMMON, chance)) return ItemRarity.UNCOMMON;

        return ItemRarity.COMMON;
    }

    private bool checkRarity(RarityDictionary dictionary, ItemRarity rarity, float currentChance)
    {
        float rarityChance;
        if (dictionary.TryGetValue(rarity, out rarityChance))
        {
            if (currentChance <= rarityChance)
            {
                return true;
            }
        }

        return false;
    }
}
