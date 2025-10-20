using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies;

public abstract class ColorStrategyBase : IColorStrategy
{
    public abstract Color CalculateColor(Vector position, Vector sizeTileMap);

    protected int ClampColor(int value) => Math.Clamp(value, 0, 255);
        
    protected int CalculateGradient(int coord, int maxCoord) => 
        (coord * 255) / maxCoord;
}