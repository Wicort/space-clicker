

using UnityEngine;

namespace Assets.Services
{
    public class AdsService: MonoBehaviour
    {
        public void DoubleDamage()
        {
            Debug.Log("Double damage started");
        }
        public void DoubleMoney()
        {
            Debug.Log("Double money started");
        }

        public void StopDoubleDamage()
        {
            Debug.Log("Double damage stopped");
        }

        public void StopDoubleMoney()
        {
            Debug.Log("Double money stopped");
        }
    }
}
