using Items;
using System;
using UnityEngine;

public class OfflineReward : MonoBehaviour
{
    private GameData _gameData;

    public int TimeSpanRestriction = 2 * 60 * 60;
    
    public float Init(GameData gameData, float damage, float enemyHealth)
    {
        _gameData = gameData;
        var lastTime = _gameData.LastPlayedTime;
        var currentTime = DateTime.UtcNow;

        if (lastTime == null) return 0;

        
        float delta = (float)Math.Clamp((currentTime - lastTime).TotalSeconds, 0, TimeSpanRestriction);
        var timeToKillEnemy = enemyHealth / damage;
        var offlineEnemyKilled = delta / timeToKillEnemy;
        Debug.Log($"offline seconds delta {delta}; \ndamage {damage}; \nhealth{enemyHealth}, \ntimeToKillEnemy {timeToKillEnemy}, \nofflineEnemyKilled {offlineEnemyKilled}");

        return offlineEnemyKilled;
    }
}
