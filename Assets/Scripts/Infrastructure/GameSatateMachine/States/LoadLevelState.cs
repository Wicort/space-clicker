using System;

namespace Assets.Scripts.Infrastructure.GameSatateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateSwitcher, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateSwitcher;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}