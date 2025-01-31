using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Arena.Character.Bulltes
{
    public class SimpleBullet : ArenaBullet
    {
        [Range(0, 10)]
        public float CoolDown = 1f;
        public float LifeTime = 5f;
        public float Speed;
        public int Damage;
        public override float GetCoolDown() => CoolDown;

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
            Vector3 newPosition = transform.position + transform.forward * Speed * Time.deltaTime;
            transform.position = new Vector3(newPosition.x, 0, newPosition.z);
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
