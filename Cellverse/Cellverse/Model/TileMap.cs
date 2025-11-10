using System.Windows;
using System.Windows.Media.Imaging;
using Cellverse.ViewModel;

namespace Cellverse.Model
{
    public class TileMap
    {
        public Vector Size { get; private set; }
        public bool CanExitBounds { get; set; }

        private byte[] _current;
        private byte[] _next;


        public HashSet<Vector> ChangedCell  = [];
        public byte[] GetCurrentArray() => _current;
        public byte[] GetNextArray() => _next;
        public TileMap(Vector size)
        {
            Size = size;
            _current = new byte[size.Y * size.X];
            _next = new byte[size.Y * size.X];
        }

        private int Index(int x, int y) => y * Size.X + x;

        private Vector GetPosition(int index)
        {
            int x = index - (index / Size.X) * Size.X; 
            int y = index / Size.X;
            return new Vector(x, y);
        }
        public byte GetCell(Vector position)
        {
            return !IsInside(position) ? (byte)0 : _current[Index(position.X, position.Y)];
        }
        public void SetCell(Vector position, byte cellId)
        {
            if (!IsInside(position)) return;
            if(_current[Index(position.X, position.Y)] != cellId)
               ChangedCell.Add(position);
            _current[Index(position.X, position.Y)] = cellId;
        }

        public void SetCells(Vector position, byte[,] brushes, byte cellId)
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

        private bool IsInside(Vector position)
        {
            return position.X >= 0 && position.Y >= 0
                                   && position.X < Size.X
                                   && position.Y < Size.Y;
        }


        public void CommitNextGeneration()
        {
            Buffer.BlockCopy(_next, 0, _current, 0, sizeof(byte) * Size.X * Size.Y);
           Array.Clear(_next, 0, _next.Length);

        }

        public void InitializeMap(bool isEmpty)
        {
            Random random = new Random();
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    var position = new Vector(x,y);
                    byte cell = isEmpty
                        ? (byte)0
                        : (random.Next(10) < 3 ? MainViewModel.Instance.MainCellType.ID : (byte)0);
                    
                    ChangedCell.Add(position);
                    SetCell(position, cell);
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
                               var position = new Vector(cellX, cellY);
                               SetCell(position, MainViewModel.Instance.MainCellType.ID);
                               
                           }
                       }
                   }
               }
           }
           finally
           {
               bitmap.Unlock();
           }
           
           for (int i = 0; i < _current.Length; i++)
           {
               ChangedCell.Add(GetPosition(i));
           }
       }


        public bool IsEmpty() => !_current.Any(c => c != 0);
        public void Resize(Vector size)
        {
            ChangedCell.Clear();
            Size = size;
            _current = new byte[size.Y * size.X];
            _next = new byte[size.Y * size.X];
        }
       
    public KeyValuePair<int, byte> GetCountNeighbours(int index, byte[] neighbourCache)
    {
        int cellsCountType = neighbourCache.Length;
        Array.Clear(neighbourCache, 0, cellsCountType); 
    
        int width = Size.X;
        int height = Size.Y;
        
        int centerX = index % width;
        int centerY = index / width;
        
        int count = 0;
    
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dx == 0 && dy == 0)
                    continue;
    
                int col = centerX + dx;
                int row = centerY + dy;
                int neighbourIndex;
    
                if (CanExitBounds)
                {
                    if (col < 0 || row < 0 || col >= width || row >= height)
                        continue;
                    
                    neighbourIndex = row * width + col;
                }
                else
                {
                    col = (col + width) % width;
                    row = (row + height) % height;
                    
                    neighbourIndex = row * width + col;
                }
    
                int cellId = _current[neighbourIndex];
                
                if (cellId == 0)
                    continue;
    
                int cacheIndex = cellId - 1;
                if ((byte)cacheIndex < (byte)cellsCountType)
                    neighbourCache[cacheIndex]++; 
    
                count++;
            }
        }
    
        byte dominantIndex = 0;
        byte maxValue = 0;
        for (byte i = 0; i < cellsCountType; i++)
        {
            if (neighbourCache[i] > maxValue)
            {
                maxValue = neighbourCache[i];
                dominantIndex = i;
            }
        }
    
        return new KeyValuePair<int, byte>(count, dominantIndex);
    }

        public void Clear()
        {
            ChangedCell.Clear();
            int deadCell = 0;

            for (int i = 0; i < _current.Length; i++)
            {
                ChangedCell.Add(GetPosition(i));
                _current[i] = 0;
            }
        }
    }
}
