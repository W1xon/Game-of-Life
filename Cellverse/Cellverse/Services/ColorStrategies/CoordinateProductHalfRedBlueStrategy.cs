using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class CoordinateProductHalfRedBlueStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte value = (byte)((position.X * position.Y) % 255);
        return Color.FromArgb(255, (byte)(value / 2), value, (byte)(value / 2));
    }
}