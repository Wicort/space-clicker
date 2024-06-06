using System.Collections.Generic;
using UnityEngine;

public class WavesHandler : MonoBehaviour
{
    [SerializeField]
    private List<Wave> waves;
    [SerializeField]
    private GameObject _enemyContainer;


    public EnemyData GetEnemyDataByLevel(int _enemyLevel)
    {
        bool isBoss = (_enemyLevel != 0 && _enemyLevel % 10 == 0);
        EnemyData enemyData = new EnemyData(_enemyLevel, isBoss);

        return enemyData;
    }
}
