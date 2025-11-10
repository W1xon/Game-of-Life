using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class PixelWaveStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector size)
    {
        double wave = Math.Sin(position.X * 0.05) * Math.Cos(position.Y * 0.05);
        byte intensity = (byte)((wave + 1) * 127.5);
        return Color.FromArgb(255, intensity, (byte)(255 - intensity), 100);
    }
}
