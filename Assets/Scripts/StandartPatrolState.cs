using System;
using UnityEngine;

[Serializable]
public class StandartPatrolState : EnemyState<StandartEnemy>
{
    private int _currentPoint;
    public StandartPatrolState(StandartEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override string NameState => "Patrol";

    public override void Enter()
    {
        _enemy.Navigation.SetSpeed(_enemy.EnemyProperty.SpeedPatrol);
        _currentPoint = 0;
        float minDistance = Vector2.Distance(_enemy.transform.position, _enemy.EnemyProperty.PatrolPoints[_currentPoint]);
        for (int i = 0; i < _enemy.EnemyProperty.PatrolPoints.Length; i++) 
        {
            float dist = Vector2.Distance(_enemy.transform.position, _enemy.EnemyProperty.PatrolPoints[i]);
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
        if (_enemy.Navigation.HasReachedDestination()) {
            int item = UnityEngine.Random.Range(0, _enemy.EnemyProperty.PatrolPoints.Length);
            if (item == _currentPoint)
                item = (item + 1) % _enemy.EnemyProperty.PatrolPoints.Length;
            _currentPoint = item;
        }

        _enemy.Navigation.UpdateTarget(_enemy.EnemyProperty.PatrolPoints[_currentPoint]);
        _enemy.Navigation.UpdateRotation(_enemy.transform);

        if (_enemy.CanSeePlayer())
            _stateMachine.ChangeState(new ChaseStandartState(_enemy, _stateMachine));
    }
}