using System;
using System.Collections;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public static Action<float> OnEnemyAttacked;

    private void Start()
    {
        StartCoroutine(Autoclick());
    }

    public void OnClick()
    {
        OnEnemyAttacked?.Invoke(GetClickDamage());
    }

    public void OnAutoClick()
    {
        OnEnemyAttacked?.Invoke(GetAutoClickDamage());
    }

    private IEnumerator Autoclick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            OnAutoClick();
        }
    }

    private float GetClickDamage()
    {
        return 20f;
    }

    private float GetAutoClickDamage()
    {
        return 0f;
    }
}
