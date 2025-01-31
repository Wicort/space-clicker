using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Arena.Character.Bulltes
{
    public abstract class ArenaBullet : MonoBehaviour
    {
        private void Update()
        {
            Move();
        }

        public abstract void Initialize(SpaceShip target);

        public abstract float GetCoolDown();

        public abstract void Move();

        public abstract void DoDamage(SpaceShip target);

        public abstract void SelfDestroy();
    }
}