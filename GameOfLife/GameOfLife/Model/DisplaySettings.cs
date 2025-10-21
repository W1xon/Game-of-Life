using System.Collections.ObjectModel;
using GameOfLife.Services;
using GameOfLife.Services.ColorStrategies;

namespace GameOfLife.Model
{
    public class DisplaySettings : ObservableObject
    {
        private int _width;
        private int _height;
        private int _cellSize;
        private bool _useCellRendering;
        private Vector _mapSize;
        private string _currentColorStrategyName;
        private ColorStrategyBase _colorStrategy;
        public event Action<Vector>? MapSizeChanged;
        
        public ObservableCollection<string> ColorStrategyNames { get; } = new()
        {
            "Gray Gradient",
            "Modified Green Gradient",
            "Half Red Blue Gradient",
            "Coordinate Product Modulo",
            "Coordinate Product Modified Green",
            "Coordinate Product Half Red Blue",
            "Trigonometric Y",
            "Trigonometric X",
            "Trigonometric Mixed",
            "Gradient XY Blue",
            "Gradient XY Green",
            "Gradient XY Red"
        };

        public int Width
        {
            get => _width;
            set
            {
                Set(ref _width, value);
                    UpdateMapSize();
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                Set(ref _height, value);
                    UpdateMapSize();
            }
        }

        public int CellSize
        {
            get => _cellSize;
            set
            {
                Set(ref _cellSize, value);
                UpdateMapSize();
            }
        }
        public bool UseCellRendering
        {
            get => _useCellRendering;
            set => Set(ref _useCellRendering, value);
        }

        public string CurrentColorStrategyName
        {
            get => _currentColorStrategyName;
            set
            {
                Set(ref _currentColorStrategyName, value);
                var index = ColorStrategyNames.IndexOf(value);
                if (index >= 0)
                    ColorStrategy = ColorStrategyRegistry.Get(index + 1); // +1, если реестр начинается с 1
            }

        }

        public ColorStrategyBase ColorStrategy
        {
            get => _colorStrategy;
            set => Set(ref _colorStrategy, value);
        }
        public Vector MapSize
        {
            get => _mapSize;
            private set => Set(ref _mapSize, value);
        }

        public int MapWidthPx => MapSize.Y * CellSize;
        public int MapHeightPx => MapSize.X * CellSize;

        public DisplaySettings(int width, int height, int cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            UpdateMapSize();
        }

        private void UpdateMapSize()
        {
            var newSize = new Vector(Width / CellSize, Height / CellSize);
            if (newSize.Equals(_mapSize)) return;

            MapSize = newSize;
            OnPropertyChanged(nameof(MapWidthPx));
            OnPropertyChanged(nameof(MapHeightPx));

            MapSizeChanged?.Invoke(MapSize);
        }

        public override string ToString() =>
            $"Window: {Width}x{Height}px | CellType: {CellSize}px | Map: {MapSize.X}x{MapSize.Y} ({MapWidthPx}x{MapHeightPx}px)";
    }
}