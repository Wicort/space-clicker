using Assets.Scripts.Infrastructure.AssetManagement;
using System;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Arena.Character.Strategy.SpaceShipBuilder
{
    public class SpaceShipBuilderFabric
    {
        public SpaceShipBuilderStrategy Get(SpaceShipBuilders type)
        {
            switch (type) 
            {
                case SpaceShipBuilders.LIGHT: return new SpaceShipBuilderStrategy(AssetPath.LightSpaceShipBuilderConfigPath);
                case SpaceShipBuilders.MEDIUM: return new SpaceShipBuilderStrategy(AssetPath.MediumSpaceShipBuilderConfigPath);
                case SpaceShipBuilders.HEAVY: return new SpaceShipBuilderStrategy(AssetPath.HeavySpaceShipBuilderConfigPath);
                default: throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public SpaceShipBuilderStrategy GetRandom()
        {
            SpaceShipBuilders type = (SpaceShipBuilders)Random.Range(0, Enum.GetNames(typeof(SpaceShipBuilders)).Length);
            return Get(type);
        }
    }
}
