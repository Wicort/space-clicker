using Assets.SaveSystem.Scripts;
using Assets.Services;
using Inventory;
using Services;
using System;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    public static GameBootstrapper Instance;

    public static Action<GameData> OnGameLoaded;

    public GameData gameData;

    private AllServices _services => AllServices.Container;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        RegisterServices();
        ISaveSystem saveSystem = _services.Single<ISaveSystem>();
        gameData = saveSystem.Load();
        OnGameLoaded?.Invoke(gameData);
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IItemService>(new ItemService());
        _services.RegisterSingle<IInventoryService>(new InventoryService());
        _services.RegisterSingle<ISaveSystem>(new PlayerPrefsSaveSystem());
    }
}
