using Assets.Scripts.AssetProvider;
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
                case SpaceShipBuilders.LIGHT: return new SpaceShipBuilderStrategy(AssetPath.LightSpaceShipBuilderConfig);
                case SpaceShipBuilders.MEDIUM: return new SpaceShipBuilderStrategy(AssetPath.MediumSpaceShipBuilderConfig);
                case SpaceShipBuilders.HEAVY: return new SpaceShipBuilderStrategy(AssetPath.HeavySpaceShipBuilderConfig);
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
