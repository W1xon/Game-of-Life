using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class StandartStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        return CellTypeRegistry.Get(1).Color;
    }
}
