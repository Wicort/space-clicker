using Assets.Scripts.Infrastructure.GameSatateMachine;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public class FollowState : ICharacterState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly CharacterStateMachineData Data;

        protected CharacterController Controller;

        private float _scannerCoolDown;

        public FollowState(IStateSwitcher stateSwitcher, CharacterStateMachineData data)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
            Controller = Data.Self.Controller;
            Data.Self.Dead += OnSelfDead;
        }

        private void OnSelfDead()
        {
            StateSwitcher.SwitchState<DeadState>();
        }

        public void Enter()
        {
            _scannerCoolDown = 1f;
            
        }

        public void Exit()
        { 
        }

        public void Update()
        {
            RotateToTarget();
            MoveToTarget();

            if (CanAttackTarget())
                AttackTarget();

            TickScannerCooldown();
            TryToStartScanner();
        }

        private void TickScannerCooldown()
        {
            _scannerCoolDown -= Time.deltaTime;
        }

        private void TryToStartScanner()
        {
            if (_scannerCoolDown <= 0f)
            {
                StateSwitcher.SwitchState<ScannerState>();
                Debug.Log($"{Data.Self.name} Start Scanning");
            }
        }

        private void MoveToTarget()
        {
            Vector3 position = Data.Self.transform.forward * Data.Velocity * Time.deltaTime;
            position = new Vector3(position.x, 0, position.z);
            Controller.Move(position);
        }

        private void RotateToTarget()
        {
            Vector3 selfPosition = Data.Self.transform.position;
            Quaternion selfRotation = Data.Self.transform.rotation;

            Vector3 targetPosition = Data.Target.transform.position;

            Quaternion rotation = Quaternion.Slerp(selfRotation,
                Quaternion.LookRotation(targetPosition - selfPosition),
                Time.deltaTime * (Data.AngularVelocity/360f));

            Data.Self.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }

        private bool CanAttackTarget()
        {
            // добавить проверку угла атаки
            return Vector3.Distance(Data.Self.transform.position, Data.Target.transform.position) < Data.AttackDistance;
        }

        private void AttackTarget()
        {
            Debug.Log($"{Data.Self.name} is Attacking {Data.Target.name} DMG({Data.AttackValue})");
            Data.Self.GetComponent<BulletSpawner>().TryToSpawnBullet();
            //Data.Target.GetDamage(Data.AttackValue);
        }
    }
}
