using System.Drawing;

namespace GameOfLife.Model;

public static class CellTypeRegistry
{
    private static readonly Dictionary<int, CellType> _types = new();

    static CellTypeRegistry()
    {
        // Стандартная Game of Life: B3/S23
        Register(new CellType(0, "", "", Color.FromArgb(25, 25, 25)));           
        Register(new CellType(1, "3", "23", Color.FromArgb(230, 230, 230)));     

        // Кастомные типы с приятной палитрой
        Register(new CellType(2, "36", "23", Color.FromArgb(255, 120, 80)));    
        Register(new CellType(3, "3", "012345678", Color.FromArgb(80, 180, 255))); 
        Register(new CellType(4, "34", "1358", Color.FromArgb(130, 255, 130)));  
        Register(new CellType(5, "345", "12", Color.FromArgb(210, 170, 255)));   
    }

    public static void Register(CellType type) => _types[type.ID] = type;
    public static CellType Get(int id) => _types.TryGetValue(id, out var type) ? type : _types[0];
}