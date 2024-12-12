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
            Debug.Log($"{config.Velocity}");
            return config.Velocity;
        }
    }
}
