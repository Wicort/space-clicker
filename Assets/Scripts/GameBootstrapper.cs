using Items;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    public static GameBootstrapper Instance;

    public static Action<GameData> OnGameLoaded;

    public GameData gameData;

    //public List<Item> items;

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
        gameData = new GameData();
        OnGameLoaded?.Invoke(gameData);
    }
}
