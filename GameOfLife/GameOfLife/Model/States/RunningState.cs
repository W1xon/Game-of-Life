using System.Drawing;
using System.Windows.Media.Imaging;

namespace GameOfLife.Model;

public class RunningState : BaseGameState
{
    public RunningState(TileMap tileMap, GameSettings gameSettings) : base(tileMap, gameSettings)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        for (int x = 0; x < _tileMap.Size.X; x++)
        {
            for (int y = 0; y < _tileMap.Size.Y; y++)
            {
                var position = new Vector(x,y);
                var neighbourCountsAndType = _tileMap.GetCountNeighbours(position, CellTypeRegistry.CountWithoutDead());
                int hasLife = _tileMap.GetCell(position);
                int indexCell = _tileMap.GetCell(position) - 1;
                if (indexCell < 0) indexCell = 0;
                _tileMap.SetNextCell(position,  _gameSettings.GetNextCellState(hasLife, neighbourCountsAndType));
            }
        }
        _tileMap.CommitNextGeneration();
    }

    public override void Exit()
    {
        Console.WriteLine("Exit Running");
    }
}
