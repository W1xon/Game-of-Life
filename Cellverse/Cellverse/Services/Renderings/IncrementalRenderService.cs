using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using Cellverse.Model;
using Cellverse.Services.ColorStrategies;

namespace Cellverse.Services;

public class IncrementalRenderService : RenderServiceBase
{

    public IncrementalRenderService(WriteableBitmap bitmap, DisplaySettings displaySettings)
    {
        _bitmap = bitmap;
        _displaySettings = displaySettings;

        var deadColor = CellTypeRegistry.Get(0).Color;
        _packedDeadColor = ToPackedColor(deadColor);
    }
    
    protected override unsafe void RenderByCells(TileMap tileMap)
{
    if (tileMap.ChangedCell.Count == 0) return;
    
    int cellSize = _displaySettings.CellSize;

    bool useStandartStrategy = _displaySettings.ColorStrategy is StandartStrategy;
    var standartStrategy = useStandartStrategy ? (StandartStrategy)_displaySettings.ColorStrategy : null;
    var otherStrategy = !useStandartStrategy ? _displaySettings.ColorStrategy : null;

    _bitmap.Lock();
    uint* buffer = (uint*)_bitmap.BackBuffer.ToPointer();
    int bitmapWidth = _bitmap.PixelWidth;

    foreach (var cell in tileMap.ChangedCell)
    {
        int xCell = cell.X;
        int yCell = cell.Y;
        int cellId = tileMap.GetCell(cell);
        uint packedColor;

        if (cellId == 0)
        {
            packedColor = _packedDeadColor;
        }
        else
        {
            Color color;
            if (useStandartStrategy)
            {
                color = standartStrategy.CalculateColor(cellId, tileMap);
            }
            else if (otherStrategy != null)
            {
                color = otherStrategy.CalculateColor(cell, tileMap.Size);
            }
            else
            {
                color = CellTypeRegistry.Get(cellId).Color;
            }
            packedColor = ToPackedColor(color);
        }

        int startX = xCell * cellSize;
        int startY = yCell * cellSize;

        for (int y = 0; y < cellSize; y++)
        {
            uint* rowStart = buffer + (startY + y) * bitmapWidth + startX;
            var rowSpan = new Span<uint>(rowStart, cellSize);
            rowSpan.Fill(packedColor);
        }

        _bitmap.AddDirtyRect(new Int32Rect(startX, startY, cellSize, cellSize));
    }

    _bitmap.Unlock();
}

}