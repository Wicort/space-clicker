using Assets.Scripts.Arena.Character;
using Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        SpaceShip getSpaceship(SpaceShipType type, Vector3 position, Quaternion rotation);
    }
}