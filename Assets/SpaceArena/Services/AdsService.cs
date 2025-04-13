using System;
using UnityEngine;

namespace Assets.Services
{
    public class AdsService : MonoBehaviour, IAdsService
    {
        public static Action OnStartDoubleDamage;
        public static Action OnStopDoubleDamage;
        public static Action OnStartDoubleMoney;
        public static Action OnStopDoubleMoney;

        public void DoubleDamage()
        {
            OnStartDoubleDamage?.Invoke();
        }
        public void DoubleMoney()
        {
            OnStartDoubleMoney?.Invoke();
        }

        public void StopDoubleDamage()
        {
            OnStopDoubleDamage?.Invoke();
        }

        public void StopDoubleMoney()
        {
            OnStopDoubleMoney?.Invoke();
        }
    }
}
