public abstract class EnemyState<EnemyClass> : IState
{
    protected readonly EnemyClass _enemy;
    protected readonly StateMachine _stateMachine;

    public abstract string NameState { get; }

    public EnemyState(EnemyClass enemy, StateMachine stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;

    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}