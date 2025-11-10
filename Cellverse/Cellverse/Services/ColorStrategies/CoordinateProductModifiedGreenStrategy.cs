using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class CoordinateProductModifiedGreenStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte value = (byte)((position.X * position.Y) % 255);
        byte r = value;
        byte g = ClampColor((int)Math.Sqrt(value));
        byte b = value;
        return Color.FromArgb(255, r, g, b);
    }
}