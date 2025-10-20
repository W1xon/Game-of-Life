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
        private readonly Dictionary<int, IColorStrategy> _colorStrategies;
        private readonly int _stride;
        private readonly System.Windows.Controls.Image _targetImage;

        public RenderingService(WriteableBitmap bitmap, System.Windows.Controls.Image targetImage, DisplaySettings displaySettings)
        {
            _bitmap = bitmap;
            _displaySettings = displaySettings;
            _targetImage = targetImage;
            _stride = bitmap.PixelWidth * (bitmap.Format.BitsPerPixel / 8);
            _colorStrategies = InitializeStrategies();
        }

        private Dictionary<int, IColorStrategy> InitializeStrategies()
        {
            return new Dictionary<int, IColorStrategy>
            {
                { 1, new GradientXGrayStrategy() },
                { 2, new GradientXModifiedGreenStrategy() },
                { 3, new GradientXHalfRedBlueStrategy() },
                { 4, new CoordinateProductModuloStrategy() },
                { 5, new CoordinateProductModifiedGreenStrategy() },
                { 6, new CoordinateProductHalfRedBlueStrategy() },
                { 7, new TrigonometricYStrategy() },
                { 8, new TrigonometricXStrategy() },
                { 9, new TrigonometricMixedStrategy() },
                { 10, new GradientXYBlueStrategy() },
                { 11, new GradientXYGreenStrategy() },
                { 12, new GradientXYRedStrategy() }
            };
        }

        public unsafe void DrawField(TileMap tileMap)
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
                    var color = CellTypeRegistry.Get(cellId).Color;

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

            // один общий dirty rect на всё поле
            _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
            _bitmap.Unlock();
        }


        public void DrawCell(Vector position, Color? color = null, int style = 1)
        {
            Color finalColor = color ?? CalculateColor(position, style);
            DrawCell(position, finalColor);
        }

        private Color CalculateColor(Vector position, int style)
        {
            if (_colorStrategies.TryGetValue(style, out var strategy))
                return strategy.CalculateColor(position, _displaySettings.MapSize);

            return _colorStrategies[1].CalculateColor(position, _displaySettings.MapSize);
        }

        public void Refresh()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _bitmap.Lock();
                _bitmap.AddDirtyRect(new Int32Rect(0, 0, _bitmap.PixelWidth, _bitmap.PixelHeight));
                _bitmap.Unlock();
                _targetImage.Source = _bitmap;
            });
        }
    }
}
