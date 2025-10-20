using GameOfLife.Services;

namespace GameOfLife.Model;

public class Game
{
    private RenderingService _renderingService;
    private StateMachine _stateMachine;

    private TileMap _tileMap;
    private RunningState _running;
    private PausedState _paused;
    private StopedState _stopped;
    private GameSettings _gameSettings;
    public Game(RenderingService renderingService, TileMap tileMap)
    {
        _tileMap = tileMap;
        _stateMachine = new StateMachine();
        _gameSettings = new GameSettings();
        _renderingService = renderingService;
        
        _running = new RunningState(_tileMap, _gameSettings);
        _paused = new PausedState(_tileMap, _gameSettings);
        _stopped = new StopedState(_tileMap, _gameSettings);
    }

    public void Start()
    {
        _tileMap.InitializeMap(false);
        _stateMachine.SetState(_running);
    }
    public void Pause() => _stateMachine.SetState(_paused);
    public void Resume() => _stateMachine.SetState(_running);
    private void Stop() => _stateMachine.SetState(_stopped);
    public void Update()
    {
        _stateMachine.Update();
        _renderingService.DrawField(_tileMap);
    }

    public void Reset()
    {
        Stop();
        
    }
}
