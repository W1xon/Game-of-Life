using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class GradientXModifiedGreenStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int value = CalculateGradient(position.X, sizeTileMap.X);
        int r = value;
        int g = ClampColor((int)Math.Sqrt(value));
        int b = value;
        return Color.FromArgb(255, r, g, b);
    }
}