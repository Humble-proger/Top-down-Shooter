using System;

[Serializable]
public class ChaseStandartState : EnemyState<StandartEnemy>
{
    public ChaseStandartState(StandartEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override string NameState => "Chase";

    public override void Enter()
    {
        _enemy.Navigation.SetSpeed(_enemy.EnemyProperty.SpeedChaise);
    }

    public override void Exit()
    {
        return;
    }

    public override void Update()
    {
        _enemy.Navigation.UpdateTarget(_enemy.Player.position);
        _enemy.Navigation.UpdateRotation(_enemy.transform);
        if (_enemy.CanAttackPlayer()) {
            _stateMachine.ChangeState(new AttackStandartState(_enemy, _stateMachine));
        }
        else if (!_enemy.CanSeePlayer()) {
            _stateMachine.ChangeState(new StandartPatrolState(_enemy, _stateMachine));
        }
    }
}