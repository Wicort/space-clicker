using Assets.Scripts.Infrastructure.GameSatateMachine.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Infrastructure.GameSatateMachine
{
    public class GameStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        

        public IState CurrentState => _currentState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new List<IState>()
            {
                new BootstrapState(this, sceneLoader),
                new LoadLevelState(this, sceneLoader),
            };

            _currentState = _states[0];
            _currentState.Enter();
            
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            if (state == null)
                throw new ArgumentException(nameof(T));

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}
