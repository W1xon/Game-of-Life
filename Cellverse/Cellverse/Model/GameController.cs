namespace Cellverse.Model;

public class GameController
{
    private StateMachine _stateMachine;
    private TileMap _tileMap;
    private RunningState _running;
    private PausedState _paused;
    private BaseGameState _stateBeforeDrawing; 
    
    public GameController(TileMap tileMap, GameSettings gameSettings)
    {
        _tileMap = tileMap;
        _stateMachine = new StateMachine();
        _running = new RunningState(_tileMap, gameSettings);
        _paused = new PausedState(_tileMap, gameSettings);
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
   
    public void BeforeDrawing()
    {
        if(_stateBeforeDrawing == null)
            _stateBeforeDrawing = _stateMachine.CurrentState;
        
        if (_stateMachine.CurrentState != _paused)
        {
            _stateMachine.SetState(_paused);
        }
    }
    
    public void AfterDrawing()
    {
        if (_stateBeforeDrawing != null)
        {
            _stateMachine.SetState(_stateBeforeDrawing);
            _stateBeforeDrawing = null; 
        }
    }
    
    public void Update()
    {
        _stateMachine.Update();
    }
    
    public void Reset()
    {
        _tileMap.Clear();
        GameSettings.DrawPosition = null;
        Pause();
    }
}