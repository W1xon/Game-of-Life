namespace GameOfLife.Model;

public class PausedBaseGameState : BaseGameState
{
    public PausedBaseGameState(TileMap tileMap, GameSettings gameSettings) : base(tileMap, gameSettings)
    {
    }

    public override void Enter()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }
}