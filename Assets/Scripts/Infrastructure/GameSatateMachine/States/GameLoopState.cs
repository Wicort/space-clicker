namespace Assets.Scripts.Infrastructure.GameSatateMachine
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {   
        }

        public void Exit()
        {
        }
    }
}
