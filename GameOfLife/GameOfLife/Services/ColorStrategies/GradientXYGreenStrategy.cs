using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class GradientXYGreenStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int r = CalculateGradient(position.X, sizeTileMap.X);
        int b = CalculateGradient(position.Y, sizeTileMap.Y);
        return Color.FromArgb(255, r, 255, b);
    }
}