using System;
using UnityEngine;

namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public class FollowState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly CharacterStateMachineData Data;

        protected CharacterController Controller;

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
            Debug.Log(GetType());
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
        }

        private void MoveToTarget()
        {
            Controller.Move(Data.Self.transform.forward * Data.Velocity * Time.deltaTime);
        }

        private void RotateToTarget()
        {
            Vector3 selfPosition = Data.Self.transform.position;
            Quaternion selfRotation = Data.Self.transform.rotation;

            Vector3 targetPosition = Data.Target.transform.position;


            Quaternion rotation = Quaternion.Slerp(selfRotation,
                Quaternion.LookRotation(targetPosition - selfPosition),
                Time.deltaTime);

            Data.Self.transform.rotation = rotation;
        }

        private bool CanAttackTarget()
        {
            return Vector3.Distance(Data.Self.transform.position, Data.Target.transform.position) < Data.AttackDistance;
        }

        private void AttackTarget()
        {
            Debug.Log($"{Data.Self.name} is Attacking {Data.Target.name} DMG({Data.AttackValue})");
            Data.Target.GetDamage(Data.AttackValue);
        }
    }
}
