using System;
using UnityEngine;

[Serializable]
public class AttackStandartState : EnemyState<StandartEnemy>
{   
    public AttackStandartState(StandartEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override string NameState => "Attack";

    public override void Enter()
    {
        _enemy.Animator.SetBool(_enemy.Walk, false);
    }

    public override void Exit()
    {
        _enemy.Animator.SetBool(_enemy.Walk, true);
    }

    public override void Update()
    {
        
        if (Time.time > _enemy.NextAttackTime) {
            _enemy.AttackPlayer();
        }

        if (!_enemy.CanAttackPlayer())
            _stateMachine.ChangeState(new ChaseStandartState(_enemy, _stateMachine));
    }
}