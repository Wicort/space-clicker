using Assets.Scripts.Infrastructure.GameSatateMachine.States;
using UnityEngine;

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
            Debug.Log("Enter game loop state");
            Clicker.OnArenaButtonClicked += GoToArena;
        }

        public void Exit()
        {
            Clicker.OnArenaButtonClicked -= GoToArena;
        }

        private void GoToArena()
        {
            Debug.Log("Go to Arena");
            _stateMachine.Enter<LoadLevelState, string>("Arena");
        }
    }
}
