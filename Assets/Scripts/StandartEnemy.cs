using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider2D))]
public class StandartEnemy : MonoBehaviour, IEnemy {

    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _takeDamageColor = Color.red;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _durationAnimation;
    [SerializeField] private AnimationCurve _curveAnimation;
    [SerializeField] private TypeEnemy _typeEnemy;

    private float _currentHealth;
    
    [HideInInspector]
    public Transform Player;
    [HideInInspector]
    public PlayerHealth PlayerHealth;
    [HideInInspector]
    public NavigationEnemy Navigation;
    [HideInInspector]
    public float NextAttackTime;
    [HideInInspector]
    public StateMachine StateMachine;
    [HideInInspector]
    public int Walk = Animator.StringToHash("Walk");

    public Animator Animator;
    public EnemyScriptableObject EnemyProperty;

    private void Awake()
    {
        StateMachine = new StateMachine();
        NextAttackTime = Time.time;
        Navigation = new NavigationEnemy(GetComponent<NavMeshAgent>());
        Navigation.SetStoppingDistance(EnemyProperty.StoppingDistance);
        StateMachine.ChangeState(new StandartPatrolState(this, StateMachine));
        Animator.SetBool(Walk, true);
        _currentHealth = EnemyProperty.Health;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerHealth = Player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public bool CanSeePlayer() => Vector2.Distance(transform.position, Player.position) <= EnemyProperty.VisibilityRange;

    public bool CanAttackPlayer() => Vector2.Distance(transform.position, Player.position) <= EnemyProperty.AttackRange;

    public void AttackPlayer() {
        PlayerHealth.TakeDamage(EnemyProperty.Damage);
        NextAttackTime = Time.time + EnemyProperty.AttackCooldown;
    }
    public void TakeDamage(float damage) {
        _currentHealth -= damage;
        if (_currentHealth < 0) {
            WaveManager.Instance.OnEnemyDied(_typeEnemy, gameObject);
            return;
        }
        Alert(Player.position);
        StartCoroutine(TakeDamageSmoothly());
    }
    private IEnumerator TakeDamageSmoothly()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _durationAnimation)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _durationAnimation;
            _spriteRenderer.color = Color.Lerp(_defaultColor, _takeDamageColor, _curveAnimation.Evaluate(normalizedPosition));
            yield return null;
        }
    }
    public void Reset()
    {
        _currentHealth = EnemyProperty.Health;
        NextAttackTime = Time.time;
        Animator.SetBool(Walk, true);
        StateMachine.ChangeState(new StandartPatrolState(this, StateMachine));
    }

    public void Alert(Vector2 point)
    {
        if (StateMachine.GetStateName() == "Patrol")
            StateMachine.ChangeState(new StandartAlertState(point, this, StateMachine));
    }
}