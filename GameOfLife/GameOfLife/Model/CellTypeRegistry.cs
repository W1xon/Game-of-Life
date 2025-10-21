using System.Drawing;

namespace GameOfLife.Model;

public static class CellTypeRegistry
{
    private static readonly Dictionary<int, CellType> _types = new();

    static CellTypeRegistry()
    {
        Register(new CellType(0, "", "", Color.FromArgb(30, 32, 36)));           // Фон — чуть светлее абсолютного чёрного
        Register(new CellType(1, "3", "23", Color.FromArgb(0, 180, 110)));       // Живые — изумрудный акцент
    }



    public static void Register(CellType type) => _types[type.ID] = type;
    public static int Count() => _types.Count();
    public static int CountWithoutDead() => _types.Count - 1;

    public static CellType Get(int id) => _types.TryGetValue(id, out var type) ? type : _types[0];
    public static CellType GetWithoutDead(int id) => _types.TryGetValue(id + 1, out var type) ? type : _types[1];

    public static void Delete(int index) => _types.Remove(index);
}