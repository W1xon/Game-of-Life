using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class TrigonometricYStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int r = CalculateGradient(position.Y, sizeTileMap.Y);
        int g = ClampColor((int)(Math.Sin(position.X) * 255));
        int b = ClampColor((int)(Math.Cos(position.X) * 255));
        return Color.FromArgb(255, r, g, b);
    }
}