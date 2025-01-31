using Assets.Scripts.Infrastructure.GameSatateMachine.States;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Infrastructure.GameSatateMachine
{
    public class GameStateMachine
    {
        private List<IExitableState> _states;
        private IExitableState _currentState;
        

        public IExitableState CurrentState => _currentState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services, LoadingCurtain curtain)
        {
            _states = new List<IExitableState>()
            {
                new BootstrapState(this, sceneLoader, services),
                new LoadLevelState(this, sceneLoader, curtain),
                new MainMenuState(this),
                new GameLoopState(this),
                new ArenaLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states.FirstOrDefault(state => state is TState) as TState;
        }
    }
}
