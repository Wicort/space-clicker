using System;

namespace Assets.Scripts.Infrastructure.GameSatateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string MenuSceneName = "MainMenu";
        private const string ArenaSceneName = "Arena";
        private const string ClickerSceneName = "Clicker";
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private string _sceneName;

        public LoadLevelState(GameStateMachine stateSwitcher, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateSwitcher;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneName = sceneName;
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            switch (_sceneName)
            {
                case MenuSceneName: _stateMachine.Enter<MainMenuState>(); break;
                case ArenaSceneName: _stateMachine.Enter<ArenaLoopState>(); break;
                case ClickerSceneName: _stateMachine.Enter<GameLoopState>(); break;
            }
        }
    }
}