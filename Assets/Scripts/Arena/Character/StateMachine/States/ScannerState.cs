using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public class ScannerState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly CharacterStateMachineData Data;

        public ScannerState(IStateSwitcher stateSwitcher, CharacterStateMachineData data)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
            //Data.Self.Damaged += OnGetDamage;
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
            if (Data.Target == null)
            {
                List<SpaceShip> targets = Data.Enemyes.Where(enemy => enemy != Data.Self).ToList();
                
                if (targets.Count == 0)
                {
                    StateSwitcher.SwitchState<WinnerState>();
                    return;
                }
                Data.Target = targets[Random.Range(0, targets.Count)];
            }

            if (Data.Target != null)
            {
                Data.Target.Dead += OnTargetIsDead;
                StateSwitcher.SwitchState<FollowState>();
                Debug.Log($"{Data.Self.name} target is {Data.Target.name}");
                return;
            }
        }

        private void OnTargetIsDead()
        {
            Data.Target = null;
            StateSwitcher.SwitchState<ScannerState>();
        }
    }
}
