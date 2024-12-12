using UnityEngine;

namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public class WinnerState : IState
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
            Debug.Log(GetType());
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
}
