using System;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    public static GameBootstrapper Instance;

    public static Action<Game> OnGameLoaded;

    public Game game;

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
        game = new Game();
        game.Currency = 5;
        game.Level = 0;
        Debug.Log($"{game}");
        OnGameLoaded?.Invoke(game);
    }
}
