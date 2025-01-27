using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameSatateMachine.States
{
    public class ArenaLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public ArenaLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log("Enter Arena Loop State");
        }

        public void Exit()
        {
            
        }
    }
}
