namespace GameOfLife.Model;

public abstract class BaseGameState : IGameState
{
    protected TileMap _tileMap;
    protected GameSettings _gameSettings;

    protected BaseGameState(TileMap tileMap, GameSettings gameSettings)
    {
        _tileMap = tileMap;
        _gameSettings = gameSettings;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    public virtual bool CanTransitionTo(IGameState newState) => true;
}