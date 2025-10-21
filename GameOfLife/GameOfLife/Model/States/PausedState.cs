namespace GameOfLife.Model;

public class PausedState : BaseGameState
{
    public PausedState(TileMap tileMap, GameSettings gameSettings) : base(tileMap, gameSettings)
    {
    }

    public override bool CanTransitionTo(IGameState newState)
    {
        return !(newState == this);
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}