using Assets.Scripts.Infrastructure.GameSatateMachine;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(new SceneLoader(this));
            DontDestroyOnLoad(this);
        }
    }
}
