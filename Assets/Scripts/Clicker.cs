using System;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public static Action<float> OnEnemyAttacked;
    public void OnClick()
    {
        Debug.Log("Clicked");
        OnEnemyAttacked?.Invoke(1f);
    }
}
