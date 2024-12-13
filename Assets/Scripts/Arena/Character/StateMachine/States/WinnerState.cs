using Assets.Scripts.Infrastructure.GameSatateMachine;
using UnityEngine;

namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public class WinnerState : ICharacterState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly CharacterStateMachineData Data;

        public WinnerState(IStateSwitcher stateSwitcher, CharacterStateMachineData data)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
        }

        public void Enter()
        {
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
}
