using System.Drawing;
using System.Windows.Media.Imaging;

namespace Cellverse.Model;

public class RunningState : BaseGameState
{
    private readonly byte[] _neighbourCache;

    public RunningState(TileMap tileMap, GameSettings gameSettings) : base(tileMap, gameSettings)
    {
        _neighbourCache = new byte[CellTypeRegistry.CountWithoutDead()];
    }

    public override void Enter()
    {
    }


    public override void Update()
    {
        unsafe
        {
            int width = _tileMap.Size.X;
            int height = _tileMap.Size.Y;
            int length = width * height;

            
            fixed (byte* pCurrent = _tileMap.GetCurrentArray())
            fixed (byte* pNext = _tileMap.GetNextArray())
            {
                for (int i = 0; i < length; i++)
                {
                    byte hasLife = pCurrent[i];
                    
                    var neighbourCountsAndType = _tileMap.GetCountNeighbours(i, _neighbourCache);
                    
                    byte nextCell = _gameSettings.GetNextCellState(hasLife, neighbourCountsAndType);
                    
                    pNext[i] = nextCell;
                    
                    if (hasLife != nextCell)
                    {
                        int x = i % width;
                        int y = i / width;
                        _tileMap.ChangedCell.Add(new Vector(x, y));
                    }
                }
            }
        }
        _tileMap.CommitNextGeneration();
    }

    public override void Exit()
    {
        Console.WriteLine("Exit Running");
    }
}
