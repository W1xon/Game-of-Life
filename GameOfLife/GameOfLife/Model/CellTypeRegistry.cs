using System.Drawing;

namespace GameOfLife.Model;

public static class CellTypeRegistry
{
    private static readonly Dictionary<int, CellType> _types = new();

    static CellTypeRegistry()
    {
        // Стандартная Game of Life: B3/S23
        Register(new CellType(0, "", "", Color.Black));        // Мертвая
        Register(new CellType(1, "3", "23", Color.White));     // Живая стандартная
        
        // Можно добавить кастомные типы
        Register(new CellType(2, "36", "23", Color.Red));      // HighLife
        Register(new CellType(3, "3", "012345678", Color.Blue)); // Maze
    }

    public static void Register(CellType type) => _types[type.ID] = type;
    public static CellType Get(int id) => _types.TryGetValue(id, out var type) ? type : _types[0];
}