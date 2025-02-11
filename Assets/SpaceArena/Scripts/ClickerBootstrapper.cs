using Assets.Scripts.Infrastructure.GameSatateMachine;
using Services;
using System;
using UnityEngine;

public class ClickerBootstrapper : MonoBehaviour
{
    public static Action<GameData> OnGameLoaded;

    
    private AllServices _services => AllServices.Container;

    public static ClickerBootstrapper Instance;
    public GameData gameData;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
            //DontDestroyOnLoad(gameObject);

            Initialize();
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Initialize()
    {
        ISaveSystem saveSystem = _services.Single<ISaveSystem>();
        gameData = saveSystem.LoadGame();
        OnGameLoaded?.Invoke(gameData);
    }
}
