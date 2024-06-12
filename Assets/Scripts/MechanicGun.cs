using UnityEngine;

public class MechanicGun : MonoBehaviour
{
    [SerializeField]
    private Transform _bulletPoint;

    private void OnEnable()
    {
        Clicker.OnPlayerClick += Attack;
    }

    private void OnDisable()
    {
        Clicker.OnPlayerClick -= Attack;
    }

    private void Attack()
    {
        Debug.Log("Attack!");
    }
}
