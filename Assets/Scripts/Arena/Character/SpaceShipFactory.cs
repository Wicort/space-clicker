using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Arena.Character
{
    public class SpaceShipFactory
    {
        private SpaceShip _playerPrfab;
        private List<SpaceShip> _enemyPrefabs;

        public SpaceShipFactory(SpaceShip playerPrfab, List<SpaceShip> enemyPrefabs)
        {
            _playerPrfab = playerPrfab;
            _enemyPrefabs = enemyPrefabs;
        }

        public SpaceShip GetPlayerSpaceShip(Vector3 position)
        {
            return get(_playerPrfab, position);
        }

        public SpaceShip GetEnemySpaceShip(Vector3 position)
        {
            return GetEnemySpaceShip(position, Quaternion.identity);
        }

        public SpaceShip GetEnemySpaceShip(Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(
                _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)],
                position,
                rotation,
                null);
        }

        private SpaceShip get(SpaceShip prefab, Vector3 position)
        {
            return GameObject.Instantiate(prefab, position, Quaternion.identity, null);
        }
    }
}
