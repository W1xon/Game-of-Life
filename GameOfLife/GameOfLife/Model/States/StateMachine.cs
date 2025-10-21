namespace GameOfLife.Model;


public class StateMachine
{
    private IGameState? _currentState;

    public IGameState? CurrentState => _currentState;

    public StateMachine()
    {
        
    }
    public StateMachine(BaseGameState initialState)
    {
        _currentState = initialState;
        _currentState.Enter();
    }

    public void SetState(BaseGameState newState)
    {
        if ((CurrentState != null && !CurrentState.CanTransitionTo(newState)))
        {
            Console.WriteLine($"Cannot transition from {CurrentState.GetType().Name} to {newState.GetType().Name}");
            return;
        }
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}

