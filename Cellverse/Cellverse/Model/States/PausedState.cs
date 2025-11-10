using Cellverse.ViewModel;

namespace Cellverse.Model;

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
        {
            var position = GameSettings.DrawPosition.Value;
            _tileMap.SetCells(position,
                MainViewModel.Instance.BrushesRegistry.SelectedBrush,
                MainViewModel.Instance.MainCellType.ID);
            GameSettings.DrawPosition = null;
        }
    }

    public override void Exit()
    {
        
    }
}