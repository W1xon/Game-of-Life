using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class GradientXGrayStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte rgb = CalculateGradient(position.X, sizeTileMap.X);
        return Color.FromArgb(255, rgb, rgb, rgb);
    }
}