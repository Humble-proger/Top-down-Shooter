public interface IState 
{
    public abstract string NameState { get; }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}