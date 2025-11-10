using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class GradientXYGreenStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte r = CalculateGradient(position.X, sizeTileMap.X);
        byte b = CalculateGradient(position.Y, sizeTileMap.Y);
        return Color.FromArgb(255, r, 255, b);
    }
}