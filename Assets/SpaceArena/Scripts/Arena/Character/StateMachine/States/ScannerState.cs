using Assets.Scripts.Infrastructure.GameSatateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public class ScannerState : ICharacterState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly CharacterStateMachineData Data;

        private TargetingStrategy _targetingStrategy = TargetingStrategy.NEAREST;

        public ScannerState(IStateSwitcher stateSwitcher, CharacterStateMachineData data)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Update()
        {
            List<SpaceShip> targets = GetEnemies();
            if (Data.Target == null)
            {
                if (CheckEndOfBattle(targets))
                {
                    StateSwitcher.SwitchState<WinnerState>();
                    return;
                }
            }
            GetNextTarget(targets);

            TryToFollowTarget();
        }

        private void TryToFollowTarget()
        {
            if (Data.Target != null)
            {
                Data.Target.Dead += OnTargetIsDead;
                StateSwitcher.SwitchState<FollowState>();
                Debug.Log($"{Data.Self.name} target is {Data.Target.name}");
                return;
            }
        }

        private List<SpaceShip> GetEnemies()
        {
            return Data.Enemyes.Where(enemy => enemy != Data.Self).ToList();
        }

        private void GetNextTarget(List<SpaceShip> targets)
        {
            switch (_targetingStrategy)
            {
                case TargetingStrategy.NEAREST: GetNearestTarget(targets); break;
                case TargetingStrategy.RANDOM: GetRandomTarget(targets); break;
                default: throw new ArgumentOutOfRangeException(nameof(_targetingStrategy));
            }
            GetRandomTarget(targets);
        }

        private void GetRandomTarget(List<SpaceShip> targets)
        {
            Data.Target = targets[Random.Range(0, targets.Count)];
        }

        private void GetNearestTarget(List<SpaceShip> targets)
        {
            if (targets.Count == 0) return;

            SpaceShip target = targets[0];
            float minimumDistance = GetDistance(target);

            foreach (SpaceShip ship in targets)
            {
                if (Data.Self.name == "Player")
                {
                    Debug.Log($"{ship.name} distance: {GetDistance(ship)}");
                }
                if ((ship != target) && (GetDistance(ship) < minimumDistance))
                {
                    target = ship;
                    minimumDistance = GetDistance(target);
                }
            }

            Data.Target = targets[Random.Range(0, targets.Count)];
        }

        private float GetDistance(SpaceShip target)
        {
            return Vector3.Distance(target.transform.position, Data.Self.transform.position);
        }

        private bool CheckEndOfBattle(List<SpaceShip> targets)
        {
            if (targets.Count == 0) 
                return true;
            
            return false;
        }

        private void OnTargetIsDead()
        {
            Data.Target = null;
            StateSwitcher.SwitchState<ScannerState>();
        }
    }

    public enum TargetingStrategy
    {
        RANDOM,
        NEAREST
    }
}
