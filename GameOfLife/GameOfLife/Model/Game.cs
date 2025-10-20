using GameOfLife.Services;

namespace GameOfLife.Model;

public class Game
{
    private RenderingService _renderingService;
    private StateMachine _stateMachine;

    private TileMap _tileMap;
    private RunningBaseGameState _running;
    private PausedBaseGameState _paused;
    private GameSettings _gameSettings;
    public Game(RenderingService renderingService, TileMap tileMap)
    {
        _tileMap = tileMap;
        _stateMachine = new StateMachine();
        _gameSettings = new GameSettings();
        _renderingService = renderingService;
        _tileMap.InitializeMap(false);
        
        _running = new RunningBaseGameState(_tileMap, _gameSettings);
        _paused = new PausedBaseGameState(_tileMap, _gameSettings);
    }

    public void Start() => _stateMachine.SetState(_running);
    public void Pause() => _stateMachine.SetState(_paused);
    public void Resume() => _stateMachine.SetState(_running);
    //public void Stop() => _stateMachine.SetState(_stopped);
    public void Update()
    {
        _stateMachine.Update();
        _renderingService.DrawField(_tileMap);
    }

    public void Reset()
    {
        
    }
}
