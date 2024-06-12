using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField]
    private WavesHandler _wavesHandler;
    [SerializeField] 
    private GameObject _enemyContainer;
    [SerializeField]
    private Canvas _enemyHUD;
    [SerializeField]
    private Text _enemyLevelText;    
    [SerializeField]
    private Image _healthBarImage;
    [SerializeField]
    private Text _healthBarText;
    [SerializeField]
    private float _maxBossTime = 10;
    [SerializeField]
    private Text _bossTimeText;
    [SerializeField]
    private Button _bosRushButton;
    
    private GameData _gameData;
    private float _enemyMaxHealth, _enemyCurrentHealth;
    private bool _isDead = false;
    private GameObject _enemyShip;
    private EnemyData _enemyData;
    private float _currentBossTime;

    public static Action<EnemyData> OnEnemyKilled;
    
    private void OnEnable()
    {
        GameBootstrapper.OnGameLoaded += Init;
        Clicker.OnEnemyAttacked += TakeDamage;        
    }

    private void OnDisable()
    {
        Clicker.OnEnemyAttacked -= TakeDamage;
        GameBootstrapper.OnGameLoaded -= Init;
    }

    private void Awake()
    {
        if (_enemyHUD == null) return;

        _enemyHUD.gameObject.SetActive(false);
    }

    private void Init(GameData gameData)
    {
        Debug.Log("Enemy handler init");
        _gameData = gameData;
        _bosRushButton.gameObject.SetActive(false);
        GetNextEnemyData();
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

        if (_currentBossTime == 0 && _bossTimeText.IsActive())
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
        OnEnemyKilled?.Invoke(_enemyData);
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
        Debug.Log("Respawn enemy");
        _enemyHUD.gameObject.SetActive(false);
        ShowLevelInfo();
        CalcEnemyParams();
        ShowNewEnemy();
        StartCoroutine(ShowNewEnemyInfo());
        if (_enemyData.IsBoss)
        {
            Debug.Log("Boss is spawned");
            _gameData.SetIsBossFailed(true);
            _currentBossTime = _maxBossTime;
            _bossTimeText.gameObject.SetActive(true);
        }
    }

    private void CalcEnemyParams()
    {
        _enemyMaxHealth = _enemyData.MaxHealth;
    }

    private void ShowLevelInfo()
    {
        string bossAlert = _enemyData.IsBoss ? " *BOSS*" : "";
        _enemyLevelText.text = $"Lvl {_gameData.Level}{bossAlert}";
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

    public void OnBossRushBtnClick()
    {
        if (!_enemyData.IsBoss && _gameData.IsBossFailed)
        {
            StartCoroutine(BossRush());
        }
    }
}
