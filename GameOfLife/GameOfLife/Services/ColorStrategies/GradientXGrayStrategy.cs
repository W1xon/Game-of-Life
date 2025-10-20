using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class GradientXGrayStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int rgb = CalculateGradient(position.X, sizeTileMap.X);
        return Color.FromArgb(255, rgb, rgb, rgb);
    }
}