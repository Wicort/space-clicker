using Assets.Scripts.Arena.Character.Bulltes;
using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    public Transform _firePoint;
    private ArenaBullet _effectToSpawn;

    private bool canAttack;

    public void Initialize(ArenaBullet effectToSpawn)
    {
        canAttack = true;
        _effectToSpawn = effectToSpawn;
    }

    private IEnumerator StartCoolDown()
    {
        yield return new WaitForSeconds(_effectToSpawn.GetCoolDown());
        canAttack = true;
    }

    public void TryToSpawnBullet()
    {
        if (canAttack)
        {
            canAttack = false;
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        ArenaBullet bullet = Instantiate(_effectToSpawn, _firePoint.transform);
        bullet.transform.SetParent(null);
        bullet.Initialize(null);
        StartCoroutine(StartCoolDown());
    }
}
