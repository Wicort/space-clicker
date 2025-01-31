using Assets.Scripts.Arena.Character.StateMachine.States;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.GameSatateMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Arena.Character.StateMachine
{
    public class CharacterStateMachine : IStateSwitcher
    {
        private List<ICharacterState> _states;
        private ICharacterState _currentState;

        public ICharacterState CurrentState => _currentState;

        public CharacterStateMachine(SpaceShip spaceShip, List<SpaceShip> enemyes)
        {
            CharacterStateMachineData data = new CharacterStateMachineData(spaceShip, enemyes);
            spaceShip.SetBulletPrefab(data.Bullet);
            _states = new List<ICharacterState>()
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
            ICharacterState state = _states.FirstOrDefault(state => state is T);

            if (state == null)
                throw new ArgumentException(nameof(T));

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();
    }
}
