using GameOfLife.ViewModel;

namespace GameOfLife.Model
{
    public class TileMap
    {
        public Vector Size { get; private set; }
        public bool CanExitBounds { get; set; }

        private int[,] _current;
        private int[,] _next;

        private int[] _neighbourCountsCache = [];

        public TileMap(Vector size)
        {
            Size = size;
            _current = new int[size.Y, size.X];
            _next = new int[size.Y, size.X];
        }

        public int GetCell(Vector position) => _current[position.Y, position.X];
        public void SetCell(Vector position, int cellId) => _current[position.Y, position.X] = cellId;

        public void SetNextCell(Vector position, int cellId) => _next[position.Y, position.X] = cellId;
        public int GetNextCell(Vector position) => _next[position.Y, position.X];

        public void CommitNextGeneration()
        {
            Buffer.BlockCopy(_next, 0, _current, 0, sizeof(int) * Size.X * Size.Y);
            Array.Clear(_next);
        }

        public void InitializeMap(bool isEmpty)
        {
            Random random = new Random();
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    _current[y, x] = isEmpty
                        ? 0
                        : (random.Next(10) < 3 ? MainViewModel.Instance.MainCellType.ID : 0);
                }
            }
        }

        public void Resize(Vector size)
        {
            Size = size;
            _current = new int[size.Y, size.X];
            _next = new int[size.Y, size.X];
        }
        public KeyValuePair<int, int> GetCountNeighbours(Vector position, int cellsCountType)
        {
            if (_neighbourCountsCache.Length < cellsCountType)
                _neighbourCountsCache = new int[cellsCountType];
            else
                Array.Clear(_neighbourCountsCache, 0, cellsCountType); 

            int count = 0;

            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int col = position.X + dx;
                    int row = position.Y + dy;

                    if (CanExitBounds)
                    {
                        if (col < 0 || row < 0 || col >= Size.X || row >= Size.Y)
                            continue;
                    }
                    else
                    {
                        col = (col + Size.X) % Size.X;
                        row = (row + Size.Y) % Size.Y;
                    }

                    int cellId = _current[row, col];
                    if (cellId == 0)
                        continue;

                    int index = cellId - 1;
                    if ((uint)index < (uint)cellsCountType)
                        _neighbourCountsCache[index]++;

                    count++;
                }
            }

            // Поиск индекса максимума
            int dominantIndex = 0;
            int maxValue = 0;
            for (int i = 0; i < cellsCountType; i++)
            {
                if (_neighbourCountsCache[i] > maxValue)
                {
                    maxValue = _neighbourCountsCache[i];
                    dominantIndex = i;
                }
            }

            return new KeyValuePair<int, int>(count, dominantIndex);
        }

    }
}
