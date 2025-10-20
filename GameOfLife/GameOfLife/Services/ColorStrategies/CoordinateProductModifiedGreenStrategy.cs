using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class CoordinateProductModifiedGreenStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int value = (position.X * position.Y) % 255;
        int r = value;
        int g = ClampColor((int)Math.Sqrt(value));
        int b = value;
        return Color.FromArgb(255, r, g, b);
    }
}