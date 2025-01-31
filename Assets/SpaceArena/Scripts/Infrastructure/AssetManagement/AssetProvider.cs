using Assets.Scripts.Arena.Character;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private SpaceShip GetEnemySpaceShip(Vector3 position, Quaternion rotation)
        {
            string prefabPath = AssetPath.ArenaEnemyPrefabPathList[Random.Range(0, AssetPath.ArenaEnemyPrefabPathList.Count)];
            SpaceShip prefab = Resources.Load<SpaceShip>(prefabPath);
            return GameObject.Instantiate(prefab, position, rotation, null);
        }

        private SpaceShip GetPlayerSpaceShip(Vector3 position, Quaternion rotation)
        {
            SpaceShip prefab = Resources.Load<SpaceShip>(AssetPath.ArenaPlayerPrefabPath);
            return GameObject.Instantiate(prefab, position, rotation, null);
        }

        public SpaceShip getSpaceship(SpaceShipType type, Vector3 position, Quaternion rotation)
        {
            switch (type)
            {
                case SpaceShipType.PLAYER:
                    return GetPlayerSpaceShip(position, rotation); 
                case SpaceShipType.ENEMY:
                    return GetEnemySpaceShip(position, rotation); 
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            
        }
    }
}
