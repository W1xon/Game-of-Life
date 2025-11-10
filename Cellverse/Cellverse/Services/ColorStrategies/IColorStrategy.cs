using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public interface IColorStrategy
{
    public Color CalculateColor(Vector position, Vector sizeTileMap);
}
