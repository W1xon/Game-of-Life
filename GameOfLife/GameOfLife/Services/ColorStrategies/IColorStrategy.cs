using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public interface IColorStrategy
{
    public Color CalculateColor(Vector position, Vector sizeTileMap);
}
