using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class TrigonometricMixedStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int r = ClampColor((int)(Math.Cos(position.X) * 255));
        int g = ClampColor((int)(Math.Sin(position.X) * 255));
        int b = CalculateGradient(position.X, sizeTileMap.X);
        return Color.FromArgb(255, r, g, b);
    }
}