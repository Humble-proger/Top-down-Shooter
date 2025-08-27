using UnityEngine;

public class StandartAlertState : EnemyState<StandartEnemy>
{
    private Vector2 _pointAlert;
    public StandartAlertState(Vector2 pointAlert,StandartEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
        _pointAlert = pointAlert;
    }

    public override string NameState => "Alert";

    public override void Enter()
    {
        _enemy.Animator.SetBool(_enemy.Walk, true);
        _enemy.Navigation.SetSpeed(_enemy.EnemyProperty.SpeedAlert);
    }

    public override void Exit()
    {
        return;
    }

    public override void Update()
    {
        if (_enemy.Navigation.HasReachedDestination())
        {
            _stateMachine.ChangeState(new StandartPatrolState(_enemy, _stateMachine));
        }

        _enemy.Navigation.UpdateTarget(_pointAlert);
        _enemy.Navigation.UpdateRotation(_enemy.transform);

        if (_enemy.CanSeePlayer())
            _stateMachine.ChangeState(new ChaseStandartState(_enemy, _stateMachine));
    }
}