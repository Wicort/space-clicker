using Assets.Scripts.Arena.Character.StateMachine.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Arena.Character.StateMachine
{
    public class CharacterStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public IState CurrentState => _currentState;

        public CharacterStateMachine(SpaceShip spaceShip, List<SpaceShip> enemyes)
        {
            CharacterStateMachineData data = new CharacterStateMachineData(spaceShip, enemyes);
            _states = new List<IState>()
            {
                new ScannerState(this, data),
                new FollowState(this, data),
                new DeadState(this, data),
                new WinnerState(this, data),
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

        public void Update() => _currentState.Update();
    }
}
