using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public class PixelWaveStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector size)
    {
        double wave = Math.Sin(position.X * 0.05) * Math.Cos(position.Y * 0.05);
        int intensity = (int)((wave + 1) * 127.5);
        return Color.FromArgb(255, intensity, 255 - intensity, 100);
    }
}
