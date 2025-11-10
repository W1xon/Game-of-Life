using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class GradientXYBlueStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte r = (byte)(CalculateGradient(position.X, sizeTileMap.Y) % 255);
        byte g = (byte)(CalculateGradient(position.Y, sizeTileMap.X) % 255);
        return Color.FromArgb(255, r, g, 255);
    }
}