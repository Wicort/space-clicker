using Assets.Scripts.Infrastructure.GameSatateMachine;
using Assets.Scripts.Infrastructure.GameSatateMachine.States;
using Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;

        public LoadingCurtain Curtain;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(new SceneLoader(this), AllServices.Container, Curtain);
            _gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}
