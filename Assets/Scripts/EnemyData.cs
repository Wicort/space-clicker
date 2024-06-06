using UnityEngine;

public class EnemyData
{
    public int EnemyLevel { get; }
    public bool IsBoss { get; }
    public GameObject Prefab { get; }
    public float MaxHealth { get; }

    public EnemyData(int enemyLevel, bool isBoss, GameObject prefab, float maxHealth)
    {
        EnemyLevel = enemyLevel;
        IsBoss = isBoss;
        Prefab = prefab;
        MaxHealth = maxHealth;
    }
}
