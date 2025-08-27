using System;

[Serializable]
public class StateMachine
{
    private IState _currentState;

    public void ChangeState(IState newState) 
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
    public string GetStateName() => _currentState.NameState;
    public void Update() 
    {
        _currentState?.Update();
    }
}
