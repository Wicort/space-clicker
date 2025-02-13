
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameSatateMachine.States
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public MainMenuState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Menu.OnStartGameButtonClicked += StartGame;
        }

        public void Exit()
        {
            Menu.OnStartGameButtonClicked -= StartGame;
        }

        private void StartGame()
        {
            Debug.Log("Start Game");
            _stateMachine.Enter<LoadLevelState, string>("Clicker");
        }

        private void OpenSettings()
        {
            Debug.Log("Open Settings");
            _stateMachine.Enter<LoadLevelState, string>("Settings");
        }
    }
}
