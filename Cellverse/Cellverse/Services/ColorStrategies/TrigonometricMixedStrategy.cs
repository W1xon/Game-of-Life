using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class TrigonometricMixedStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte r = ClampColor((int)(Math.Cos(position.X) * 255));
        byte g = ClampColor((int)(Math.Sin(position.X) * 255));
        byte b = CalculateGradient(position.X, sizeTileMap.X);
        return Color.FromArgb(255, r, g, b);
    }
}