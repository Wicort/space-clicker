using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Arena.Character.Bulltes
{
    public class SimpleBullet : ArenaBullet
    {
        public float LifeTime = 5f;
        public float Speed;
        public int Damage;

        public override void DoDamage(SpaceShip target)
        {
            target.GetDamage(Damage);
        }

        public override void Initialize(SpaceShip target)
        {
            StartCoroutine(LifeCicle());
        }

        private IEnumerator LifeCicle()
        {
            yield return new WaitForSeconds(LifeTime);
            SelfDestroy();
        }

        public override void Move()
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        public override void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SpaceShip target))
            {
                DoDamage(target);
                SelfDestroy();
            }
        }
    }
}
