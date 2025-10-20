using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class GradientXHalfRedBlueStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int value = CalculateGradient(position.X, sizeTileMap.X);
        return Color.FromArgb(255, value / 2, value, value / 2);
    }
}