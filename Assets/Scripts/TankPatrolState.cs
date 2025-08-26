using System;
using UnityEngine;

[Serializable]
public class TankPatrolState : EnemyState<TankEnemy>
{
    private int _currentPoint;
    public TankPatrolState(TankEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        _enemy.Navigation.SetSpeed(_enemy.SpeedPatrol);
        _currentPoint = 0;
        float minDistance = Vector2.Distance(_enemy.transform.position, _enemy.PatrolPoints[_currentPoint]);
        for (int i = 0; i < _enemy.PatrolPoints.Length; i++) 
        {
            float dist = Vector2.Distance(_enemy.transform.position, _enemy.PatrolPoints[i]);
            if (dist < minDistance) {
                _currentPoint = i;
                minDistance = dist;
            }
        }
    }

    public override void Exit()
    {
        return;
    }

    public override void Update()
    {
        if (_enemy.Navigation.HasReachedDestination())
            _currentPoint = (_currentPoint + 1) % _enemy.PatrolPoints.Length;

        _enemy.Navigation.UpdateTarget(_enemy.PatrolPoints[_currentPoint]);
        _enemy.Navigation.UpdateRotation(_enemy.transform);

        if (_enemy.CanSeePlayer())
            _stateMachine.ChangeState(new ChaseTankState(_enemy, _stateMachine));
    }
}