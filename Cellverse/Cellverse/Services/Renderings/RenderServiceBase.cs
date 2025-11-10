using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cellverse.Model;
using Cellverse.Services.ColorStrategies;
using Model_Vector = Cellverse.Model.Vector;
using Vector = Cellverse.Model.Vector;

namespace Cellverse.Services;

public abstract class RenderServiceBase
{
    protected  DisplaySettings _displaySettings;
    protected  WriteableBitmap _bitmap;
    protected  uint _packedDeadColor;
    protected abstract void RenderByCells(TileMap tileMap);
    public void Render(TileMap tileMap)
    {
        if (_displaySettings.UseCellRendering)
            RenderByCells(tileMap);
        else
            RenderByPixels(tileMap); 
        tileMap.ChangedCell.Clear();
    }

    protected unsafe void RenderByPixels(TileMap tileMap)
    {
        int cellSize = _displaySettings.CellSize;
        int width = tileMap.Size.X;
        int height = tileMap.Size.Y;

        _bitmap.Lock();
        uint* buffer = (uint*)_bitmap.BackBuffer.ToPointer();
        int bitmapWidth = _bitmap.PixelWidth;

        for (int yCell = 0; yCell < height; yCell++)
        {
            for (int xCell = 0; xCell < width; xCell++)
            {
                int cellId = tileMap.GetCell(new Model_Vector(xCell, yCell));
                var baseColor = CellTypeRegistry.Get(cellId).Color;

                int startX = xCell * cellSize;
                int startY = yCell * cellSize;

                for (int y = 0; y < cellSize; y++)
                {
                    int rowOffset = (startY + y) * bitmapWidth + startX;

                    for (int x = 0; x < cellSize; x++)
                    {
                        int absX = startX + x;
                        int absY = startY + y;

                        Color finalColor;

                        if (cellId == 0)
                        {
                            finalColor = CellTypeRegistry.Get(0).Color;
                        }
                        else if (_displaySettings.ColorStrategy is StandartStrategy standartStrategy)
                        {
                            finalColor = standartStrategy.CalculateColor(new Model_Vector(xCell, yCell), tileMap);
                        }
                        else if (_displaySettings.ColorStrategy != null)
                        {
                            finalColor = _displaySettings.ColorStrategy.CalculateColor(
                                new Model_Vector(absX, absY),
                                new Model_Vector(_bitmap.PixelWidth, _bitmap.PixelHeight));
                        }
                        else
                        {
                            finalColor = baseColor;
                        }

                        buffer[rowOffset + x] = ToPackedColor(finalColor);
                    }
                }
            }
        }

        _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
        _bitmap.Unlock();
    }

    public unsafe void RenderFull(TileMap tileMap)
    { 
        tileMap.ChangedCell.Clear();
        int cellSize = _displaySettings.CellSize;
        int width = tileMap.Size.X;
        int height = tileMap.Size.Y;
        
        bool useStandartStrategy = _displaySettings.ColorStrategy is StandartStrategy;
        var otherStrategy = !useStandartStrategy ? _displaySettings.ColorStrategy : null;

        _bitmap.Lock();
        uint* buffer = (uint*)_bitmap.BackBuffer.ToPointer();
        int bitmapWidth = _bitmap.PixelWidth;

        for (int yCell = 0; yCell < height; yCell++)
        {
            for (int xCell = 0; xCell < width; xCell++)
            {
                var cell = new Model_Vector(xCell, yCell);
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
                        color = CellTypeRegistry.Get(cellId).Color;
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
            }
        } 

        _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
        _bitmap.Unlock();
    }
    public unsafe void Clear()
    {
        _bitmap.Lock();
        uint* buffer = (uint*)_bitmap.BackBuffer.ToPointer();
        int totalPixels = _bitmap.PixelWidth * _bitmap.PixelHeight;

        var bufferSpan = new Span<uint>(buffer, totalPixels);
        bufferSpan.Fill(_packedDeadColor);

        _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
        _bitmap.Unlock();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static uint ToPackedColor(Color color)
    {
        return (uint)(color.B | (color.G << 8) | (color.R << 16) | (255 << 24));
    }
}