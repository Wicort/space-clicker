
using Items;
using Services;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class Drone : MonoBehaviour
{
    public GameObject droneObject;

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
    

    private void Initialize(GameData gameData)
    {
        _gameData = gameData;
        if (_gameData.DroneIsReady)
        {
            droneObject.SetActive(true);
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
        _gameData.DroneIsReady = true;
        _saveSystem.Save(_gameData);
    }
}
