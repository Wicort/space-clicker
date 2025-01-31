using System;

namespace Assets.Services
{
    public interface IAdsService
    {
        public static Action OnStartDoubleDamage;
        public static Action OnStopDoubleDamage;
        public static Action OnStartDoubleMoney;
        public static Action OnStopDoubleMoney;

        void DoubleDamage();
        void DoubleMoney();
        void StopDoubleDamage();
        void StopDoubleMoney();
    }
}