using Assets.SaveSystem.Scripts;
using Assets.Services;
using Inventory;
using Services;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameSatateMachine.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private AllServices _services => AllServices.Container;

        public BootstrapState(GameStateMachine stateSwitcher, SceneLoader sceneLoader)
        {
            _stateMachine = stateSwitcher;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("MainMenu");
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IItemService>(new ItemService());
            _services.RegisterSingle<IInventoryService>(new InventoryService());
            _services.RegisterSingle<ISaveSystem>(new PlayerPrefsSaveSystem(_services.Single<IInventoryService>(), _services.Single<IItemService>()));
        }
    }
}
