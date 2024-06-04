using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField]
    private float _enemyBaseHealth, _growthRate, _bossMultiplier;
    [SerializeField]
    public float _baseReward, _rewardGrowthRate, bossRewardMultiplier;
    [SerializeField]
    private Text _enemyLevelText;    
    [SerializeField]
    private Image _healthBarImage;
    [SerializeField]
    private Text _healthBarText;
    [SerializeField]
    private GameObject _enemyShip;

    private int _enemyLevel;
    private float _enemyMaxHealth, _enemyCurrentHealth;
    private bool _isDead = false;
    private bool _isBoss = false;
    

    private void OnEnable()
    {
        Clicker.OnEnemyAttacked += TakeDamage;
    }

    private void OnDisable()
    {
        Clicker.OnEnemyAttacked -= TakeDamage;
    }

    private void Start()
    {
        EnemyData enemyData = new EnemyData(0, false);
        Init(enemyData);
    }

    private void Init(EnemyData enemyData)
    {
        _enemyLevel = enemyData.EnemyLevel;
        _isBoss = enemyData.IsBoss;

        RespawnEnemy();
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
        var reward = CalculateReward();
        yield return new WaitForSeconds(1f);

        RespawnEnemy();
    }

    private void DestroyEnemyShip()
    {
        _enemyShip.SetActive(false);
    }

    private void RespawnEnemy()
    {
        _enemyLevel++;
        ShowLevelInfo();
        CalcEnemyParams();
        ShowNewEnemy();
        StartCoroutine(ShowNewEnemyInfo());
    }

    private void CalcEnemyParams()
    {
        _enemyMaxHealth = _enemyBaseHealth * Mathf.Pow(1 + _growthRate, _enemyLevel - 1);
        if (_isBoss) _enemyMaxHealth *= _bossMultiplier;
    }

    private void ShowLevelInfo()
    {
        _enemyLevelText.text = $"Lvl {_enemyLevel}";
    }

    private void ShowNewEnemy()
    {
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

    private float CalculateReward()
    {
        float calculatedReward = _baseReward * Mathf.Pow(1 + _rewardGrowthRate, _enemyLevel - 1);
        if (_isBoss)
        {
            calculatedReward *= bossRewardMultiplier;
        }
        return calculatedReward;
    }
}
