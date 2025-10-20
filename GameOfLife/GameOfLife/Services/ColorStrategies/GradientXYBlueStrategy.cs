using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class GradientXYBlueStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int r = CalculateGradient(position.X, sizeTileMap.Y);
        int g = CalculateGradient(position.Y, sizeTileMap.X);
        return Color.FromArgb(255, r, g, 255);
    }
}