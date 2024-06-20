using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
