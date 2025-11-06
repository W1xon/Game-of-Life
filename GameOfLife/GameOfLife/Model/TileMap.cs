using System.Windows;
using System.Windows.Media.Imaging;
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

        public int GetCell(Vector position)
        {
            return !IsInside(position) ? 0 : _current[position.Y, position.X];
        }
        public void SetCell(Vector position, int cellId)
        {
            if (!IsInside(position)) return;
            _current[position.Y, position.X] = cellId;
        }

        public void SetCells(Vector position, int[,] brushes, int cellId)
        {
            int width = brushes.GetLength(1);
            int height = brushes.GetLength(0);

            int offsetX = width / 2;
            int offsetY = height / 2;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (brushes[y, x] == 1)
                    {
                        SetCell(new Vector(position.X + x - offsetX, position.Y + y - offsetY), cellId);
                    }
                }
            }
        }


        public void SetNextCell(Vector position, int cellId)
        {
            if (!IsInside(position)) return;
            _next[position.Y, position.X] = cellId;
        }
        public int GetNextCell(Vector position)
        {
            return !IsInside(position) ? 0 : _next[position.Y, position.X];
        }

        private bool IsInside(Vector position)
        {
            return position is { X: >= 0, Y: >= 0 }
                   && position.X < _current.GetLength(1)
                   && position.Y < _current.GetLength(0);
        }
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

       public void InitializeMap(WriteableBitmap bitmap)
       {
           int cellSize = MainViewModel.Instance.DisplaySettings.CellSize;
       
           int width = bitmap.PixelWidth;
           int height = bitmap.PixelHeight;
       
           // Блокировка для безопасного доступа к пикселям
           bitmap.Lock();
       
           try
           {
               unsafe
               {
                   int bytesPerPixel = (bitmap.Format.BitsPerPixel + 7) / 8;
                   byte* pStart = (byte*)bitmap.BackBuffer;
       
                   for (int y = 0; y < height; y += cellSize)
                   {
                       for (int x = 0; x < width; x += cellSize)
                       {
                           int totalBrightness = 0;
                           int count = 0;
       
                           // Проходим по пикселям блока
                           for (int py = y; py < y + cellSize && py < height; py++)
                           {
                               for (int px = x; px < x + cellSize && px < width; px++)
                               {
                                   byte* pPixel = pStart + py * bitmap.BackBufferStride + px * bytesPerPixel;
                                   byte b = pPixel[0];
                                   byte g = pPixel[1];
                                   byte r = pPixel[2];
       
                                   // Яркость как среднее RGB
                                   int brightness = (r + g + b) / 3;
                                   totalBrightness += brightness;
                                   count++;
                               }
                           }
       
                           int averageBrightness = count > 0 ? totalBrightness / count : 255;
       
                           // Если блок светлый, считаем клетку живой
                           if (averageBrightness > 60)
                           {
                               int cellX = x / cellSize;
                               int cellY = y / cellSize;
                               SetCell(new Vector(cellX, cellY), MainViewModel.Instance.MainCellType.ID);
                           }
                       }
                   }
               }
           }
           finally
           {
               bitmap.Unlock();
           }
       }


        public bool IsEmpty() => !_current.Cast<int>().Any(c => c != 0);
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
