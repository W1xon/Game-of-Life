using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class TrigonometricXStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte r = ClampColor((int)(Math.Cos(position.X) * 255));
        byte g = CalculateGradient(position.X, sizeTileMap.X);
        byte b = ClampColor((int)(Math.Sin(sizeTileMap.X) * 255));
        return Color.FromArgb(255, r, g, b);
    }
}