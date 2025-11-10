using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cellverse.Model;
using Cellverse.Services.ColorStrategies;
using Vector = Cellverse.Model.Vector;

namespace Cellverse.Services;

public class StandardRenderService : RenderServiceBase
{
    public StandardRenderService(WriteableBitmap bitmap, DisplaySettings displaySettings)
    {
        _bitmap = bitmap;
        _displaySettings = displaySettings;

        var deadColor = CellTypeRegistry.Get(0).Color;
        _packedDeadColor = ToPackedColor(deadColor);
    }

    protected override unsafe void RenderByCells(TileMap tileMap)
    {
        int cellSize = _displaySettings.CellSize;
        int width = tileMap.Size.X;
        int height = tileMap.Size.Y;
        
        bool useStandartStrategy = _displaySettings.ColorStrategy is StandartStrategy;
        var standartStrategy = useStandartStrategy ? (StandartStrategy)_displaySettings.ColorStrategy : null;
        var otherStrategy = !useStandartStrategy ? _displaySettings.ColorStrategy : null;

        _bitmap.Lock();
        uint* buffer = (uint*)_bitmap.BackBuffer.ToPointer();
        int bitmapWidth = _bitmap.PixelWidth;

        for (int yCell = 0; yCell < height; yCell++)
        {
            for (int xCell = 0; xCell < width; xCell++)
            {
                int cellId = tileMap.GetCell(new Vector(xCell, yCell));
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
                        color = standartStrategy.CalculateColor(new Vector(xCell, yCell), tileMap);
                    }
                    else if (otherStrategy != null)
                    {
                        color = otherStrategy.CalculateColor(new Vector(xCell, yCell), tileMap.Size);
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
            }
        } 

        _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
        _bitmap.Unlock();
    }
}
