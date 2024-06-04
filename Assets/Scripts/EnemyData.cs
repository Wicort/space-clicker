public class EnemyData
{
    public int EnemyLevel { get; }
    public bool IsBoss { get; }

    public EnemyData(int enemyLevel, bool isBoss)
    {
        EnemyLevel = enemyLevel;
        IsBoss = isBoss;
    }
}
