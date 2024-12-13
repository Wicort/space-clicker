using Assets.Scripts.Infrastructure.GameSatateMachine.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Infrastructure.GameSatateMachine
{
    public class GameStateMachine
    {
        private List<IExitableState> _states;
        private IExitableState _currentState;
        

        public IExitableState CurrentState => _currentState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new List<IExitableState>()
            {
                new BootstrapState(this, sceneLoader),
                new LoadLevelState(this, sceneLoader),
            };
            
        }

        public void Enter<TState>() where TState : class, IState
        {
            _currentState?.Exit();
            IState state = GetState<TState>();
            _currentState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            IPayloadState<TPayload> state = GetState<TState>();

            _currentState.Exit();
            _currentState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states.FirstOrDefault(state => state is TState) as TState;
        }
    }
}
