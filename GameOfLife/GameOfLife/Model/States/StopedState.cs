namespace GameOfLife.Model;

public class StopedState : BaseGameState
{
    public StopedState(TileMap tileMap, GameSettings gameSettings) : base(tileMap, gameSettings)
    {
    }
    public override bool CanTransitionTo(IGameState newState)
    {
        return !(newState == this);
    }

    public override void Enter()
    {
        _tileMap.InitializeMap(true);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}