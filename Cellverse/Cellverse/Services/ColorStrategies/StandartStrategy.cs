using System.Runtime.CompilerServices;
using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.Services.ColorStrategies;

public class StandartStrategy : ColorStrategyBase
{
    public override Color CalculateColor(Vector position, Vector sizeTileMap)
    {
        return CellTypeRegistry.Get(1).Color;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public  Color CalculateColor(Vector position, TileMap tileMap)
    {
        return CellTypeRegistry.Get(tileMap.GetCell(position)).Color;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public  Color CalculateColor(int cellId, TileMap tileMap)
    {
        return CellTypeRegistry.Get(cellId).Color;
    }
}
