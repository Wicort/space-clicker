using Assets.Scripts.Arena.Character.Bulltes;
using Assets.Scripts.Arena.Character.Strategy.SpaceShipBuilder;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Arena.Character.StateMachine
{
    public class CharacterStateMachineData
    {
        public SpaceShip Self;
        public SpaceShip Target;
        public List<SpaceShip> Enemyes;
        public int Velocity;
        public int AngularVelocity;
        public int MaxHealthPoints;
        public int HealthPoints;
        public int AttackDistance;
        public int AttackValue;
        public ArenaBullet Bullet;
        

        public CharacterStateMachineData(SpaceShip self, List<SpaceShip> enemyes)
        {
            SpaceShipBuilderFabric fabric = new SpaceShipBuilderFabric();
            SpaceShipBuilderStrategy strategy;
            Self = self;
            Enemyes = enemyes;
            if (self.name == "Player")
                strategy = fabric.GetMedium();
            else
                strategy = fabric.GetRandom();

            Velocity = strategy.GetVelocity();
            AngularVelocity = strategy.GetAngularVelocity();
            MaxHealthPoints = Random.Range(100, 150);
            HealthPoints = MaxHealthPoints;
            AttackDistance = Random.Range(9, 10);
            AttackValue = Random.Range(10, 50);
            Bullet = strategy.GetBullet();
            self.SetBulletPrefab(Bullet);
            Target = null;
            Self.Damaged += OnGetDamage;
        }
        private void OnGetDamage(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            HealthPoints -= value;
            if (HealthPoints < 0)
                HealthPoints = 0;

            if (HealthPoints == 0)
                Kill();
        }

        public void Kill()
        {
            Self.Kill();
        }
    }
}
