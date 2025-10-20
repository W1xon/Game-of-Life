using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class CoordinateProductHalfRedBlueStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int value = (position.X * position.Y) % 255;
        return Color.FromArgb(255, value / 2, value, value / 2);
    }
}