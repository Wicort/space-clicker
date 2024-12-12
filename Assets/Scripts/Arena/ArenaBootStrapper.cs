using Assets.Scripts.Arena.Character;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBootStrapper : MonoBehaviour
{
    [SerializeField, Range(2,15)] private int _playerCount = 5;
    [SerializeField] private List<SpaceShip> _enemyPrefabs;
    [SerializeField] private SpaceShip _playerPrefab;
    [SerializeField] private List<SpaceShip> _targets;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private SpaceShipFactory _spaceShipFactory;

    public List<SpaceShip> Targets => _targets;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        _targets = new List<SpaceShip>();

        _spaceShipFactory = new SpaceShipFactory(_playerPrefab, _enemyPrefabs);
        Vector3 playerPosition = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));

        SpaceShip player = _spaceShipFactory.GetPlayerSpaceShip(playerPosition);
        player.name = "Player";
        _camera.Follow = player.gameObject.transform;

        _targets.Add(player);

        for(int i = 0; i < _playerCount - 1; i++)
        {
            Vector3 enemyPosition = new Vector3(Random.Range(-50f, 50f) , 0f, Random.Range(-50f, 50f));
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
            enemy.GetDamage(150);
        }
    }
}
