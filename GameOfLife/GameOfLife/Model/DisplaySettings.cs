namespace GameOfLife.Model;

public class DisplaySettings
{
    public int Width { get; }
    public int Height { get; }
    public int CellSize { get; }

    public Vector MapSize { get; private set; }

    public int MapWidthPx => MapSize.Y * CellSize;
    public int MapHeightPx => MapSize.X * CellSize;

    public DisplaySettings(int width, int height, int cellSize)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        MapSize = new Vector(width / cellSize, height / cellSize);
    }

    public override string ToString()
    {
        return $"Window: {Width}x{Height}px | CellType: {CellSize}px | Map: {MapSize.X}x{MapSize.Y} ({MapWidthPx}x{MapHeightPx}px)";
    }
}