using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class GradientXYRedStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int g = CalculateGradient(position.X, sizeTileMap.X);
        int b = CalculateGradient(position.Y, sizeTileMap.Y);
        return Color.FromArgb(255, 255, g, b);
    }
}