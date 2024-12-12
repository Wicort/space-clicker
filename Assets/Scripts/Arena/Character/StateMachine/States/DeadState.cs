using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public class DeadState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly CharacterStateMachineData Data;

        public DeadState(IStateSwitcher stateSwitcher, CharacterStateMachineData data)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
        }

        private IEnumerator DestroySpaceShip()
        {
            yield return new WaitForSeconds(1f);
            GameObject.Destroy(Data.Self.gameObject);
        }

        public void Enter()
        {
            Debug.Log(GetType());
            Data.Enemyes.Remove(Data.Self);
            Data.Self.StartCoroutine(DestroySpaceShip());
        }

        public void Exit()
        {
         
        }

        public void Update()
        {
         
        }

    }
}
