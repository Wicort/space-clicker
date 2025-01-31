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
        Bullet bullet = Instantiate(_bulletPrefab, transform);
        bullet.Init(targetPosition);
    }
}
