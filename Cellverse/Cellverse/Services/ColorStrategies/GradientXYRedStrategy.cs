using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class GradientXYRedStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte g = CalculateGradient(position.X, sizeTileMap.X);
        byte b = CalculateGradient(position.Y, sizeTileMap.Y);
        return Color.FromArgb(255, 255, g, b);
    }
}