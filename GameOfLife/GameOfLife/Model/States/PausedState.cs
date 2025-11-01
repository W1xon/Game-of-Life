using GameOfLife.ViewModel;

namespace GameOfLife.Model;

public class PausedState : BaseGameState
{
    public PausedState(TileMap tileMap, GameSettings gameSettings) : base(tileMap, gameSettings)
    {
    }

    public override bool CanTransitionTo(IGameState newState)
    {
        return newState != this;
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        if(GameSettings.DrawPosition.HasValue)
            _tileMap.SetCells(GameSettings.DrawPosition.Value,
                MainViewModel.Instance.BrushesRegistry.SelectedBrush,
                MainViewModel.Instance.MainCellType.ID);
    }

    public override void Exit()
    {
        
    }
}