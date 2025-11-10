using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class GradientXHalfRedBlueStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte value = CalculateGradient(position.X, sizeTileMap.X);
        return Color.FromArgb(255, (byte)(value / 2), value, (byte)(value / 2));
    }
}