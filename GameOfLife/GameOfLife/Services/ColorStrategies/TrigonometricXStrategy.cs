using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class TrigonometricXStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int r = ClampColor((int)(Math.Cos(position.X) * 255));
        int g = CalculateGradient(position.X, sizeTileMap.X);
        int b = ClampColor((int)(Math.Sin(sizeTileMap.X) * 255));
        return Color.FromArgb(255, r, g, b);
    }
}