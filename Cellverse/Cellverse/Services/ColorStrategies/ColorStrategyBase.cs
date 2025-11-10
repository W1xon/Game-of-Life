using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public abstract class ColorStrategyBase : IColorStrategy
{
    public abstract Color CalculateColor(Vector position, Vector sizeTileMap);

    protected byte ClampColor(int value) => (byte)Math.Clamp(value, 0, 255);
        
    protected byte CalculateGradient(int coord, int maxCoord) => 
        (byte)((coord * 255) / maxCoord);
}