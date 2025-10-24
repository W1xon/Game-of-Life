namespace GameOfLife.Model;

public class Game
{
    
    private StateMachine _stateMachine;
    private TileMap _tileMap;
    private RunningState _running;
    private PausedState _paused;
    private StopedState _stopped;
    private BaseGameState _lastState;
    public Game( TileMap tileMap, GameSettings gameSettings)
    {
        _tileMap = tileMap;
        _stateMachine = new StateMachine();
        _running = new RunningState(_tileMap, gameSettings);
        _paused = new PausedState(_tileMap, gameSettings);
        _stopped = new StopedState(_tileMap, gameSettings);
    }

    public void Start()
    {
        if(_tileMap.IsEmpty())
            _tileMap.InitializeMap(false);
        _stateMachine.SetState(_running);
    }
    public void Pause()
    {
        _stateMachine.SetState(_paused);
    }
    public void Resume() => _stateMachine.SetState(_running);
    private void Stop() => _stateMachine.SetState(_stopped);

    public void PauseAfterDraw()
    {
        if (_stateMachine.CurrentState == _paused) return;
        _lastState = _stateMachine.CurrentState;
        _stateMachine.SetState(_paused);
    }

    public void ResumeBeforeDraw()
    {
        _stateMachine.SetState(_lastState);
    }
    
    public void Update()
    {
        _stateMachine.Update();
    }

    public void Reset()
    {
        Stop();
        
    }
}
