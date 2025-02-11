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
            Clicker.OnMenuButtonClicked += GoToMenu;
        }

        public void Exit()
        {
            Clicker.OnArenaButtonClicked -= GoToArena;
            Clicker.OnMenuButtonClicked -= GoToMenu;
        }

        private void GoToArena()
        {
            _stateMachine.Enter<LoadLevelState, string>("Arena");
        }

        private void GoToMenu()
        {
            _stateMachine.Enter<LoadLevelState, string>("MainMenu");
        }
    }
}
