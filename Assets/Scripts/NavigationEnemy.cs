using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class NavigationEnemy 
{
    private readonly NavMeshAgent _agent;

    public NavigationEnemy(NavMeshAgent agent) 
    {
        _agent = agent;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.autoRepath = true;
        _agent.stoppingDistance = 0.5f;
    }
    public void UpdateTarget(Vector2 target) {
        Vector3 vector = (Vector3)target;
        _agent.SetDestination(vector);
    }

    public void SetSpeed(float speed) 
    {
        _agent.speed = speed;
    }

    public void SetStoppingDistance(float value) {  
        _agent.stoppingDistance = value;
    }

    public void UpdateRotation(Transform transform)
    {
        if (_agent.velocity != Vector3.zero)
        {
            // Поворот в направлении движения
            float angle = Mathf.Atan2(_agent.velocity.y, _agent.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public bool HasReachedDestination() {
        return _agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending;
    }
}