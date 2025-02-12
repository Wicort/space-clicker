using Assets.Scripts.Arena.Character;
using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.Services;
using Assets.SpaceArena.SaveSystem.Scripts;
using Assets.SpaceArena.Scripts.Infrastructure.Localization;
using Cinemachine;
using Inventory;
using Services;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBootStrapper : MonoBehaviour
{
    private const float SpawnRadius = 50f;
    [SerializeField, Range(2,15)] private int _playerCount = 5;
    [SerializeField] private List<SpaceShip> _enemyPrefabs;
    [SerializeField] private SpaceShip _playerPrefab;
    [SerializeField] private List<SpaceShip> _targets;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private SpaceShipFactory _spaceShipFactory;
    private AllServices _container => AllServices.Container;

    public List<SpaceShip> Targets => _targets;
    

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        _targets = new List<SpaceShip>();

        RegisterServices();

        _spaceShipFactory = new SpaceShipFactory(_container.Single<IAssetProvider>());
        Vector3 playerPosition = new Vector3(Random.Range(-SpawnRadius, SpawnRadius), 0f, Random.Range(-SpawnRadius, SpawnRadius));

        SpaceShip player = _spaceShipFactory.GetPlayerSpaceShip(playerPosition);
        player.name = "Player";
        _camera.Follow = player.gameObject.transform;

        _targets.Add(player);

        for(int i = 0; i < _playerCount - 1; i++)
        {
            Vector3 enemyPosition = new Vector3(Random.Range(-SpawnRadius, SpawnRadius) , 0f, Random.Range(-SpawnRadius, SpawnRadius));
            Quaternion enemyRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            SpaceShip enemy = _spaceShipFactory.GetEnemySpaceShip(enemyPosition, enemyRotation);
            enemy.name = $"Enemy_{i}";
            _targets.Add(enemy);

        }
        foreach(SpaceShip target in _targets)
        {
            target.Initialize(_targets);
        }
    }
    private void Update()
    {
        if (_targets.Count <= 1) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceShip enemy = _targets[Random.Range(0, _targets.Count)];
            //enemy.GetDamage(150);
        }
    }
    private void RegisterServices()
    {
        ILocalizationService localizationService = new EnLocalizationService();

        _container.RegisterSingle<ILocalizationService>(localizationService);
        _container.RegisterSingle<IItemService>(new ItemService(localizationService));
        _container.RegisterSingle<IInventoryService>(new InventoryService());
        _container.RegisterSingle<IAssetProvider>(new AssetProvider());
        _container.RegisterSingle<ISaveSystem>(new PlayerPrefsSaveSystem(_container.Single<IInventoryService>(), _container.Single<IItemService>()));

    }
}
