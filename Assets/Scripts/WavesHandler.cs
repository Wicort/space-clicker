using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WavesHandler : MonoBehaviour
{
    [SerializeField]
    private List<Wave> waves;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _enemyBaseHealth, _growthRate, _bossMultiplier;



    public EnemyData GetEnemyDataByLevel(int enemyLevel)
    {
        int waveNumber = Mathf.Min((int)Mathf.Floor((enemyLevel - 1) / 10f), waves.Count - 1);

        Debug.Log($"wave {waveNumber}, calc {Mathf.Floor((enemyLevel - 1) / 10f)}, count {waves.Count - 1}");

        Wave currentWave = waves[waveNumber] ;

        bool isBoss = (enemyLevel > 0 && enemyLevel % 10 == 0);
        Debug.Log($"currentWave.EnemiePrefabs.Count) {currentWave.EnemiePrefabs.Count}");

        GameObject enemyPrefab = isBoss ? currentWave.BossPrefab : currentWave.EnemiePrefabs[Random.Range(0, currentWave.EnemiePrefabs.Count)];

        float _enemyMaxHealth = _enemyBaseHealth * Mathf.Pow(1 + _growthRate, enemyLevel - 1);
        if (isBoss) _enemyMaxHealth *= _bossMultiplier;

        EnemyData enemyData = new EnemyData(enemyLevel, isBoss, enemyPrefab, _enemyMaxHealth);
        
        return enemyData;
    }
}
