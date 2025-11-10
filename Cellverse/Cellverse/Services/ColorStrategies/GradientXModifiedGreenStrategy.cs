using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class GradientXModifiedGreenStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte value = CalculateGradient(position.X, sizeTileMap.X);
        byte r = value;
        byte g = ClampColor((int)Math.Sqrt(value));
        byte b = value;
        return Color.FromArgb(255, r, g, b);
    }
}