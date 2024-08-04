using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandler : MonoBehaviour
{
    [SerializeField]
    private WavesHandler _wavesHandler;

    private void OnEnable()
    {
        EnemyHandler.OnEnemyKilled += getDrop;
    }

    private void getDrop(EnemyData enemy)
    {
        Wave currentWave = _wavesHandler.getWave(enemy.EnemyLevel);
        GetDrop(enemy.EnemyLevel, currentWave);
    }

    public List<Item> GetDrop(int enemyLevel, Wave wave)
    {
        List<Item> drop = new List<Item>();
        
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
                Debug.Log("DROP!");
            }
        }

        return drop;
    }
}
