using Assets.Scripts.Infrastructure.AssetManagement;
using UnityEngine;

namespace Assets.Scripts.Arena.Character
{
    public class SpaceShipFactory
    {
        private readonly IAssetProvider _assets;

        public SpaceShipFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public SpaceShip GetPlayerSpaceShip(Vector3 position)
        {
            return _assets.getSpaceship(SpaceShipType.PLAYER, position, Quaternion.identity);
        }

        public SpaceShip GetEnemySpaceShip(Vector3 position, Quaternion rotation)
        {
            return _assets.getSpaceship(SpaceShipType.ENEMY, position, rotation);
        }

        
    }
}
