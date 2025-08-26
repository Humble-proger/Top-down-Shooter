using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider2D))]
public class TankEnemy : MonoBehaviour, IEnemy {
    
    [HideInInspector]
    public NavigationEnemy Navigation;
    [HideInInspector]
    public float NextAttackTime;
    [HideInInspector]
    public StateMachine StateMachine;

    public Transform Player;
    public PlayerHealth PlayerHealth;
    public float SpeedChaise;
    public float SpeedPatrol;
    public float VisibilityRange;
    public float Damage;
    public float Health;
    public float AttackRange;
    public float AttackCooldown;
    public Vector2[] PatrolPoints;

    private void Awake()
    {
        StateMachine = new StateMachine();
        NextAttackTime = Time.time;
        Navigation = new NavigationEnemy(GetComponent<NavMeshAgent>());
        StateMachine.ChangeState(new TankPatrolState(this, StateMachine));
        
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public bool CanSeePlayer() => Vector2.Distance(transform.position, Player.position) <= VisibilityRange;

    public bool CanAttackPlayer() => Vector2.Distance(transform.position, Player.position) <= AttackRange;

    public void AttackPlayer() {
        PlayerHealth.TakeDamage(Damage);
        NextAttackTime = Time.time + AttackCooldown;
    }
    public void TakeDamage(float damage) {
        Health -= damage;
        if (Health < 0) {
            Destroy(gameObject);
        }
    }
}