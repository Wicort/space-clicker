using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _target;
    private float _moveSpeed = 1;
    public void Init(Vector3 target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null) return;
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _moveSpeed);
        var dist = transform.position - _target;
        if (dist.sqrMagnitude < _moveSpeed)
        {
            Destroy(gameObject);
        }
    }
}
