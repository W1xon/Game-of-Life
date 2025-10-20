using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class CoordinateProductModuloStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        int rgb = (position.X * position.Y) % 255;
        return Color.FromArgb(255, rgb, rgb, rgb);
    }
}