using Assets.SpaceArena.Scripts.Infrastructure.Localization;
using DamageNumbersPro;
using Services;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    private const float _timeForNewEnemy = 2f;
    [SerializeField]
    private WavesHandler _wavesHandler;
    [SerializeField] 
    private GameObject _enemyContainer;
    [SerializeField]
    private Canvas _enemyHUD;
    [SerializeField]
    private Text _enemyLevelText;    
    [SerializeField]
    private Slider _healthBarSlider;
    [SerializeField]
    private Text _healthBarText;
    [SerializeField]
    private float _maxBossTime = 30;
    [SerializeField]
    private Text _bossTimeText;
    [SerializeField]
    private Button _bosRushButton;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private DamageNumber damageNumberPrefab;
    
    private GameData _gameData;
    private float _enemyMaxHealth, _enemyCurrentHealth;
    private bool _isDead = false;
    private GameObject _enemyShip;
    private EnemyData _enemyData;
    private float _currentBossTime;
    private ISaveSystem _saveSystem;
    private ILocalizationService _localizationService;

    public static Action<EnemyData, float> OnEnemyKilled;
    
    private void OnEnable()
    {
        ClickerBootstrapper.OnGameLoaded += Init;
        Clicker.OnEnemyAttacked += TakeDamage;        
    }

    private void OnDisable()
    {
        Clicker.OnEnemyAttacked -= TakeDamage;
        ClickerBootstrapper.OnGameLoaded -= Init;
    }

    private void Awake()
    {
        if (_enemyHUD == null) return;

        _enemyHUD.gameObject.SetActive(false);
    }

    private void Init(GameData gameData)
    {
        _saveSystem = AllServices.Container.Single<ISaveSystem>();
        _localizationService = AllServices.Container.Single<ILocalizationService>();
        _gameData = gameData;
        GetNextEnemyData();
        _bosRushButton.gameObject.SetActive(_gameData.IsBossFailed && !_enemyData.IsBoss);
        RespawnEnemy();
    }

    private void Update()
    {
        if (_enemyData == null) return;

        if (_currentBossTime > 0)
        {
            _currentBossTime -= Time.deltaTime;
        } else
        {
            _currentBossTime = 0;
        }

        if (_enemyData.IsBoss)
        {
            _bossTimeText.text = Mathf.Ceil(_currentBossTime).ToString();
        }

        if (_enemyData.IsBoss && _currentBossTime == 0 && _bossTimeText.IsActive())
        {
            StartCoroutine(FailBoss());
        }
    }

    private IEnumerator FailBoss()
    {
        Debug.Log("Boss failed");
        DestroyEnemyShip();
        _bossTimeText.gameObject.SetActive(false);
        _gameData.SetIsBossFailed(true);
        _gameData.PrevLevel();
        GetNextEnemyData();
        yield return new WaitForSeconds(1f);
        RespawnEnemy();
        _bosRushButton.gameObject.SetActive(true);
    }

    private IEnumerator BossRush()
    {
        Debug.Log("Boss rush!!!");
        _bosRushButton.gameObject.SetActive(false);
        DestroyEnemyShip();
        _gameData.SetIsBossFailed(false);
        GetNextEnemyData();
        yield return new WaitForSeconds(1f);
        RespawnEnemy();
        
    }

    private void GetNextEnemyData()
    {
        _enemyHUD.gameObject.SetActive(false);
        _gameData.NextLevel();
        _enemyData = _wavesHandler.GetEnemyDataByLevel(_gameData.Level);        
    }

    private void TakeDamage(float dmg)
    {
        if (dmg <= 0 || _isDead) return;

        if (_enemyShip != null) 
            damageNumberPrefab.Spawn(_enemyShip.transform.position, ShortScaleString.parseDouble(dmg, 1, 1000, true));

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
        if (_enemyData.IsBoss)
        {
            _gameData.SetIsBossFailed(false);
            _bossTimeText.gameObject.SetActive(false);
        }
        DestroyEnemyShip();
        OnEnemyKilled?.Invoke(_enemyData, 1);
        GetNextEnemyData();
        yield return new WaitForSeconds(_timeForNewEnemy);
        //_saveSystem.SaveGame();
        RespawnEnemy();
    }

    private void DestroyEnemyShip()
    {
        if (!_enemyShip) return; 

        GameObject explosion = Instantiate(_explosionPrefab, _enemyShip.transform);
        explosion.transform.parent = null;
        Destroy(_enemyShip);
    }

    private void RespawnEnemy()
    {
        _enemyHUD.gameObject.SetActive(false);
        ShowLevelInfo();
        CalcEnemyParams();
        ShowNewEnemy();
        StartCoroutine(ShowNewEnemyInfo());
        if (_enemyData.IsBoss)
        {
            _gameData.SetIsBossFailed(true);
            _currentBossTime = _maxBossTime;
            _bossTimeText.gameObject.SetActive(true);
        }
        _saveSystem.SaveGame();
    }

    private void CalcEnemyParams()
    {
        _enemyMaxHealth = _enemyData.MaxHealth;
    }

    private void ShowLevelInfo()
    {
        string bossAlert = _enemyData.IsBoss ? $" *{_localizationService.GetUIByKey("BOSS")}*" : "";
        _enemyLevelText.text = $"{_localizationService.GetUIByKey("Lvl")}. {_gameData.Level}{bossAlert}";
    }

    private void ShowNewEnemy()
    {
        _enemyShip = Instantiate(_enemyData.Prefab, _enemyContainer.transform);
        _enemyShip.SetActive(true);

        if (_enemyData.IsBoss )
        {
            _currentBossTime = _maxBossTime;
        }
    }

    private IEnumerator ShowNewEnemyInfo()
    {
        yield return new WaitForSeconds(.2f);
        _enemyCurrentHealth = _enemyMaxHealth;
        _isDead = false;
        RefreshHealthBar();
        _enemyHUD.gameObject.SetActive(true);
    }


    private void RefreshHealthBar()
    {
        //_healthBarSlider. = _enemyCurrentHealth/_enemyMaxHealth;
        _healthBarSlider.value = _enemyCurrentHealth / _enemyMaxHealth;
        if (_isDead)
        {
            _healthBarText.text = _localizationService.GetUIByKey("Dead");
        }
        else
        {
            _healthBarText.text = $"{ShortScaleString.parseDouble(Mathf.Ceil(_enemyCurrentHealth), 1, 1000, true)} / {ShortScaleString.parseDouble(Mathf.Ceil(_enemyMaxHealth), 1, 1000, true)}";
        }
    }

    public void OnBossRushBtnClick()
    {
        if (!_enemyData.IsBoss && _gameData.IsBossFailed)
        {
            StartCoroutine(BossRush());
        }
    }

    public Vector3 GetEnemyPosition()
    {
        if (_enemyShip != null)
          return _enemyShip.transform.position;
        else return Vector3.one;
    }

    public float GetCurrentEnemyHealth()
    {
        return _enemyData.MaxHealth;
    }

    public float GetCurrentEnemyLevel()
    {
        return _enemyData.EnemyLevel;
    }

    public void OfflineKilling(float coeff)
    {
        OnEnemyKilled?.Invoke(_enemyData, coeff);
    }
}
