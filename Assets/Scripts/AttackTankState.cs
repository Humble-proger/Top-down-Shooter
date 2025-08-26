using System;
using UnityEngine;

[Serializable]
public class AttackTankState : EnemyState<TankEnemy>
{   
    public AttackTankState(TankEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void Enter()
    {
        return;
    }

    public override void Exit()
    {
        return;
    }

    public override void Update()
    {
        
        if (Time.time > _enemy.NextAttackTime) {
            _enemy.AttackPlayer();
        }

        if (!_enemy.CanAttackPlayer())
            _stateMachine.ChangeState(new ChaseTankState(_enemy, _stateMachine));
    }
}