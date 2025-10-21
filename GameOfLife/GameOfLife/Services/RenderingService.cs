using System.Drawing;
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
        private readonly int _stride;

        public RenderingService(WriteableBitmap bitmap, DisplaySettings displaySettings)
        {
            _bitmap = bitmap;
            _displaySettings = displaySettings;
            _stride = bitmap.PixelWidth * (bitmap.Format.BitsPerPixel / 8);
        }
        public unsafe void Clear()
        {
            var color = CellTypeRegistry.Get(0).Color;

            _bitmap.Lock();
            byte* buffer = (byte*)_bitmap.BackBuffer.ToPointer();

            for (int y = 0; y < _bitmap.PixelHeight; y++)
            {
                byte* pixel = buffer + y * _stride;
                for (int x = 0; x < _bitmap.PixelWidth; x++)
                {
                    pixel[0] = color.B;
                    pixel[1] = color.G;
                    pixel[2] = color.R;
                    pixel[3] = 255;
                    pixel += 4;
                }
            }

            _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
            _bitmap.Unlock();
        }

        public void DrawField(TileMap tileMap)
        {
            if(_displaySettings.UseCellRendering)
                DrawFieldByCell(tileMap);
            else DrawFieldByPixel(tileMap);
            
        }
        private unsafe void DrawFieldByPixel(TileMap tileMap)
        {
        int cellSize = _displaySettings.CellSize;
        int width = tileMap.Size.X;
        int height = tileMap.Size.Y;
    
        _bitmap.Lock();
        byte* buffer = (byte*)_bitmap.BackBuffer.ToPointer();
    
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
                    int row = (startY + y) * _stride;
                    byte* pixel = buffer + row + startX * 4;
    
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
    
                        pixel[0] = finalColor.B;
                        pixel[1] = finalColor.G;
                        pixel[2] = finalColor.R;
                        pixel[3] = 255;
                        pixel += 4;
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

            _bitmap.Lock();
            byte* buffer = (byte*)_bitmap.BackBuffer.ToPointer();

            for (int yCell = 0; yCell < height; yCell++)
            {
                for (int xCell = 0; xCell < width; xCell++)
                {
                    int cellId = tileMap.GetCell(new Vector(xCell, yCell));
                    Color color;
                    if (cellId == 0)
                    {
                        color = CellTypeRegistry.Get(0).Color;
                    }
                    else if (_displaySettings.ColorStrategy is StandartStrategy standartStrategy)
                    {
                        color = standartStrategy.CalculateColor(new Vector(xCell, yCell), tileMap);
                    }

                    else
                        color = _displaySettings.ColorStrategy.CalculateColor(new Vector(xCell,yCell), tileMap.Size);
                    int startX = xCell * cellSize;
                    int startY = yCell * cellSize;

                    // рисуем блок клеток одним цветом
                    for (int y = 0; y < cellSize; y++)
                    {
                        int row = (startY + y) * _stride;
                        byte* pixel = buffer + row + startX * 4;

                        for (int x = 0; x < cellSize; x++)
                        {
                            pixel[0] = color.B;
                            pixel[1] = color.G;
                            pixel[2] = color.R;
                            pixel[3] = 255;
                            pixel += 4;
                        }
                    }
                }
            }

            _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
            _bitmap.Unlock();
        }
    }
   
}
