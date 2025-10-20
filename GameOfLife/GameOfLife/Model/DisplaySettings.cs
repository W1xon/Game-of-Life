namespace GameOfLife.Model
{
    public class DisplaySettings : ObservableObject
    {
        private int _width;
        private int _height;
        private int _cellSize;
        private Vector _mapSize;

        // Событие при изменении размеров карты
        public event Action<Vector>? MapSizeChanged;

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