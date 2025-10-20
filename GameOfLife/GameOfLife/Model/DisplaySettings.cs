namespace GameOfLife.Model;

public class DisplaySettings
{
    public int WindowWidth { get; }
    public int WindowHeight { get; }
    public int CellSize { get; }

    public Vector MapSize { get; private set; }

    public int MapWidthPx => MapSize.Y * CellSize;
    public int MapHeightPx => MapSize.X * CellSize;

    public DisplaySettings(int windowWidth, int windowHeight, int cellSize)
    {
        WindowWidth = windowWidth;
        WindowHeight = windowHeight;
        CellSize = cellSize;
        MapSize = new Vector(windowHeight / cellSize, windowWidth / cellSize);
    }

    public override string ToString()
    {
        return $"Window: {WindowWidth}x{WindowHeight}px | CellType: {CellSize}px | Map: {MapSize.X}x{MapSize.Y} ({MapWidthPx}x{MapHeightPx}px)";
    }
}