using System;

[Serializable]
public class ChaseTankState : EnemyState<TankEnemy>
{
    public ChaseTankState(TankEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        _enemy.Navigation.SetSpeed(_enemy.SpeedChaise);
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
            _stateMachine.ChangeState(new AttackTankState(_enemy, _stateMachine));
        }
        else if (!_enemy.CanSeePlayer()) {
            _stateMachine.ChangeState(new TankPatrolState(_enemy, _stateMachine));
        }
    }
}