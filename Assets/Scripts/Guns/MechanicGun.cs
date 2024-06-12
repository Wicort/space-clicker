using UnityEngine;

public class MechanicGun : MonoBehaviour
{
    [SerializeField]
    private Transform _bulletPoint;
    [SerializeField]
    private Bullet _bulletPrefab;

    private void OnEnable()
    {
        Clicker.OnPlayerClick += Attack;
    }

    private void OnDisable()
    {
        Clicker.OnPlayerClick -= Attack;
    }

    private void Attack(Vector3 targetPosition)
    {
        Debug.Log("Attack!");
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.Init(targetPosition);
    }
}
