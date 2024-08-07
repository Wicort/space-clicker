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

    private IItemService _itemService;
    private IInventoryService _inventoryService;

    private void OnEnable()
    {
        EnemyHandler.OnEnemyKilled += getDrop;
    }

    private void Start()
    {
        
    }

    private void getDrop(EnemyData enemy)
    {
        Wave currentWave = _wavesHandler.getWave(enemy.EnemyLevel);
        GetDrop(enemy.EnemyLevel, currentWave);
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
            if (isBoss) currentChance *= wave.BossDropMultiplier;
            Debug.Log($"current chance: {currentChance}");
            if (currentChance > dropChance)
            {
                Item item = _itemService.GetRandomItemByRarity(CalcRarity(wave.RarityDropChance));
                _inventoryService.AddItems("Player", item.Id, 1);
                Debug.Log($"DROP! {item.Name} added");
            }
        }

        return drop;
    }

    public ItemRarity CalcRarity(RarityDictionary dictionary)
    {
        var chance = UnityEngine.Random.Range(0f, 100f);
        //dictionary<


        return ItemRarity.COMMON;
    }
}
