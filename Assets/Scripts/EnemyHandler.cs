using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField]
    private WavesHandler _wavesHandler;
    [SerializeField] 
    private GameObject _enemyContainer;
    [SerializeField]
    private Text _enemyLevelText;    
    [SerializeField]
    private Image _healthBarImage;
    [SerializeField]
    private Text _healthBarText;
    
    

    private Game _gameData;
    private float _enemyMaxHealth, _enemyCurrentHealth;
    private bool _isDead = false;
    private bool _isBoss = false;
    private GameObject _enemyShip;
    private EnemyData _enemyData;

    public static Action<int> OnEnemyKilled;
    

    private void OnEnable()
    {
        Clicker.OnEnemyAttacked += TakeDamage;
        GameBootstrapper.OnGameLoaded += Init;
    }

    private void OnDisable()
    {
        Clicker.OnEnemyAttacked -= TakeDamage;
        GameBootstrapper.OnGameLoaded -= Init;
    }

    private void Init(Game gameData)
    {
        _gameData = gameData;
        GetNextEnemyData();
        RespawnEnemy();
    }

    private void GetNextEnemyData()
    {
        _gameData.NextLevel();
        _enemyData = _wavesHandler.GetEnemyDataByLevel(_gameData.Level);
        _isBoss = _enemyData.IsBoss;
    }

    private void TakeDamage(float dmg)
    {
        if (dmg <= 0 || _isDead) return;

        _enemyCurrentHealth -= dmg;

        if (_enemyCurrentHealth <= 0 )
        {
            _enemyCurrentHealth = 0;
            _isDead = true;
            StartCoroutine(KillEnemy());
        }
        RefreshHealthBar();
    }

    private IEnumerator KillEnemy()
    {
        DestroyEnemyShip();
        OnEnemyKilled?.Invoke(_gameData.Level);
        GetNextEnemyData();
        yield return new WaitForSeconds(1f);

        RespawnEnemy();
    }

    private void DestroyEnemyShip()
    {
        Destroy(_enemyShip);
    }

    private void RespawnEnemy()
    {
        ShowLevelInfo();
        CalcEnemyParams();
        ShowNewEnemy();
        StartCoroutine(ShowNewEnemyInfo());
    }

    private void CalcEnemyParams()
    {
        _enemyMaxHealth = _enemyData.MaxHealth;
    }

    private void ShowLevelInfo()
    {
        _enemyLevelText.text = $"Lvl {_gameData.Level}";
    }

    private void ShowNewEnemy()
    {
        _enemyShip = Instantiate(_enemyData.Prefab, _enemyContainer.transform);
        _enemyShip.SetActive(true);
    }

    private IEnumerator ShowNewEnemyInfo()
    {
        yield return new WaitForSeconds(.2f);
        _enemyCurrentHealth = _enemyMaxHealth;
        _isDead = false;
        RefreshHealthBar();
    }


    private void RefreshHealthBar()
    {
        _healthBarImage.fillAmount = _enemyCurrentHealth/_enemyMaxHealth;
        if (_isDead)
        {
            _healthBarText.text = "Dead";
        }
        else
        {
            _healthBarText.text = $"{Mathf.Ceil(_enemyCurrentHealth)} / {Mathf.Ceil(_enemyMaxHealth)}";
        }
    }
}
