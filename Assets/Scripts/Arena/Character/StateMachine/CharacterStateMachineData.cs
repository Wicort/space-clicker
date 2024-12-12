using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Arena.Character.StateMachine
{
    public class CharacterStateMachineData
    {
        public SpaceShip Self;
        public SpaceShip Target;
        public int Velocity = Random.Range(1, 5);
        public List<SpaceShip> Enemyes;
        public int MaxHealthPoints;
        public int HealthPoints;
        public int AttackDistance;
        public int AttackValue;
        

        public CharacterStateMachineData(SpaceShip self, List<SpaceShip> enemyes)
        {
            Self = self;
            Enemyes = enemyes;
            Velocity = Random.Range(35, 75);
            MaxHealthPoints = Random.Range(100, 150);
            HealthPoints = MaxHealthPoints;
            AttackDistance = Random.Range(9, 10);
            AttackValue = Random.Range(10, 50);

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
