using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class TrigonometricYStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        byte r = CalculateGradient(position.Y, sizeTileMap.Y);
        byte g = ClampColor((int)(Math.Sin(position.X) * 255));
        byte b = ClampColor((int)(Math.Cos(position.X) * 255));
        return Color.FromArgb(255, r, g, b);
    }
}