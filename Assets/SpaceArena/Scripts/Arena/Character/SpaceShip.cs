using Assets.Scripts.Arena.Character.Bulltes;
using Assets.Scripts.Arena.Character.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SpaceShip : MonoBehaviour
{
    public Action<int> Damaged;
    public Action Dead;
    public BulletSpawner BulletSpawner;
    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;

    public CharacterController Controller => _characterController;


    public void Initialize(List<SpaceShip> enemyes)
    {
        _characterController = GetComponent<CharacterController>();
        _stateMachine = new CharacterStateMachine(this, enemyes);
    }

    public void SetBulletPrefab(ArenaBullet bulletPrefab)
    {
        BulletSpawner.Initialize(bulletPrefab);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void GetDamage(int value)
    {
        Damaged?.Invoke(value);
    }

    public void Kill()
    {
        Dead?.Invoke();
    }
}
