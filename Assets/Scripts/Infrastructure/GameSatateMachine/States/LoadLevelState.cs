namespace Assets.Scripts.Infrastructure.GameSatateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateSwitcher, SceneLoader sceneLoader)
        {
            _stateMachine = stateSwitcher;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName);

        public void Exit()
        {
            
        }
    }
}