using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class CoordinateProductModuloStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte rgb = (byte)((position.X * position.Y) % 255);
        return Color.FromArgb(255, rgb, rgb, rgb);
    }
}