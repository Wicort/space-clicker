using Assets.SaveSystem.Scripts;
using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.Services;
using Assets.SpaceArena.Scripts.Infrastructure.Localization;
using Inventory;
using Services;
using YG;

namespace Assets.Scripts.Infrastructure.GameSatateMachine.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private AllServices _services;

        public BootstrapState(GameStateMachine stateSwitcher, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateSwitcher;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {   
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
            ILocalizationService localizationService;
            //YG2.SwitchLanguage("en");

            if (YG2.lang == "ru")
                localizationService = new RuLocalizationService();
            else
                localizationService = new EnLocalizationService();

            _services.RegisterSingle<ILocalizationService>(localizationService);
            _services.RegisterSingle<IItemService>(new ItemService(localizationService));
            _services.RegisterSingle<IInventoryService>(new InventoryService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<ISaveSystem>(new PlayerPrefsSaveSystem(_services.Single<IInventoryService>(), _services.Single<IItemService>()));
            
        }
    }
}
