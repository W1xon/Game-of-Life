using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using GameOfLife.Model;
using GameOfLife.Services.ColorStrategies;
using Vector = GameOfLife.Model.Vector;

namespace GameOfLife.Services
{
    public class RenderingService
    {
        private readonly WriteableBitmap _bitmap;
        private readonly DisplaySettings _displaySettings;
        private readonly uint _packedDeadColor; 

        public RenderingService(WriteableBitmap bitmap, DisplaySettings displaySettings)
        {
            _bitmap = bitmap;
            _displaySettings = displaySettings;

            var deadColor = CellTypeRegistry.Get(0).Color;
            _packedDeadColor = PackColor(deadColor);
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

        public void DrawField(TileMap tileMap)
        {
            if (_displaySettings.UseCellRendering)
                DrawFieldByCell(tileMap);
            else
                DrawFieldByPixel(tileMap); 
        }

        private unsafe void DrawFieldByPixel(TileMap tileMap)
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
                    int cellId = tileMap.GetCell(new Vector(xCell, yCell));
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
                                finalColor = standartStrategy.CalculateColor(new Vector(xCell, yCell), tileMap);
                            }
                            else if (_displaySettings.ColorStrategy != null)
                            {
                                finalColor = _displaySettings.ColorStrategy.CalculateColor(
                                    new Vector(absX, absY),
                                    new Vector(_bitmap.PixelWidth, _bitmap.PixelHeight));
                            }
                            else
                            {
                                finalColor = baseColor;
                            }

                            buffer[rowOffset + x] = PackColor(finalColor);
                        }
                    }
                }
            }

            _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
            _bitmap.Unlock();
        }


        private unsafe void DrawFieldByCell(TileMap tileMap)
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
                        packedColor = PackColor(color);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint PackColor(Color color)
        {
             return (uint)(color.B | (color.G << 8) | (color.R << 16) | (255 << 24));
        }
    }
}