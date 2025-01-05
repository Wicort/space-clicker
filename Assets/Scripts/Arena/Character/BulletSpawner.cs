using Assets.Scripts.Arena.Character.Bulltes;
using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    public Transform _firePoint;
    [Range(0, 10)]
    public float coolDown;
    public ArenaBullet effectToSpawn;

    private bool canAttack;

    private void Awake()
    {
        canAttack = true;
    }

    private void Update()
    {
        if (canAttack)
        {
            canAttack = false;
            ArenaBullet bullet = Instantiate(effectToSpawn, _firePoint.transform);
            bullet.transform.SetParent(null);
            bullet.Initialize(null);
            StartCoroutine(StartCoolDown());
        }
    }

    private IEnumerator StartCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
}
