
using Items;
using Services;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class Drone : MonoBehaviour
{
    [SerializeField] private GameObject droneObject;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;

    private ISaveSystem _saveSystem;
    private GameData _gameData;

    private void Awake()
    {
        _saveSystem = AllServices.Container.Single<ISaveSystem>();
    }

    private void OnEnable()
    {
        ClickerBootstrapper.OnGameLoaded += Initialize;
    }
    private void OnDisable()
    {
        ClickerBootstrapper.OnGameLoaded -= Initialize;
        Clicker.OnAutoclick -= Attack;
    }

    private void Initialize(GameData gameData)
    {
        _gameData = gameData;
        if (_gameData.DroneIsReady)
        {
            droneObject.SetActive(true);
            Clicker.OnAutoclick += Attack;
        } else
        {
            droneObject.SetActive(false);
            ActiveUpgrade.OnFirstDroneUpgrade += ShowDrone;
        }
    }

    private void ShowDrone()
    {
        droneObject.SetActive(true);
        ActiveUpgrade.OnFirstDroneUpgrade -= ShowDrone;
        Clicker.OnAutoclick += Attack;
        _gameData.DroneIsReady = true;
        _saveSystem.SaveGame();
    }

    private void Attack(Vector3 targetPosition)
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform);
        bullet.Init(targetPosition);
    }
}
