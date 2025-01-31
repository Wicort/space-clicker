using Assets.Scripts.Arena.Character.Bulltes;
using UnityEngine;

namespace Assets.Scripts.Arena.Character.Strategy.SpaceShipBuilder
{
    public class SpaceShipBuilderStrategy : ISpaceShipBuilderStrategy
    {
        private SpaceShipBuilderConfig config;

        public SpaceShipBuilderStrategy(string assetPath)
        {
            config = Resources.Load<SpaceShipBuilderConfig>(assetPath);
        }

        public int GetVelocity()
        {
            float coeff = GetCoeff();
            return (int)(config.Velocity + config.Velocity * coeff);
        }

        public int GetAngularVelocity()
        {
            float coeff = GetCoeff();
            return (int)(config.AngularVelocity + config.AngularVelocity * coeff);
        }

        public ArenaBullet GetBullet()
        {
            Debug.Log($"Bullet prefab asset path: {config.BulletPrefabAssetPath}");
            ArenaBullet prefab = Resources.Load<ArenaBullet>(config.BulletPrefabAssetPath);
            Debug.Log($"prefab: {prefab}");
            return prefab;
        }

        private float GetCoeff()
        {
            return Random.Range(-config.RandomCoefficient, config.RandomCoefficient);
        }
    }
}
