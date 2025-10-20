using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public interface IColorStrategy
{
    Color CalculateColor(Vector position, Vector sizeTileMap);
}
