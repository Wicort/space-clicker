namespace Assets.Scripts.Infrastructure.GameSatateMachine.States
{
    public class LoadLevelState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(IStateSwitcher stateSwitcher, SceneLoader sceneLoader)
        {
            _stateSwitcher = stateSwitcher;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load("MainMenu");
        }

        public void Exit()
        {
            
        }
    }
}