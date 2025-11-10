using System.Windows.Media;

namespace Cellverse.Model;

public static class CellTypeRegistry
{
    private static readonly Dictionary<int, CellType> _types = new();

    static CellTypeRegistry()
    {
        Register(new CellType(
            id: 0,
            birthRules: "",
            survivalRules: "",
            color: Color.FromArgb(255, 25, 28, 33),
            name: "Пустая"
        ));

        Register(new CellType(
            id: 1,
            birthRules: "3",
            survivalRules: "23",
            color: Color.FromArgb(255, 80, 180, 255),
            name: "Обычная",
            isPriority: true
        ));

        Register(new CellType(
            id: 2,
            birthRules: "36",
            survivalRules: "23",
            color: Color.FromArgb(255, 255, 100, 170),
            name: "HighLife"
        ));

        Register(new CellType(
            id: 3,
            birthRules: "3",
            survivalRules: "012345678",
            color: Color.FromArgb(255, 140, 255, 110),
            name: "Бессмертная"
        ));

        Register(new CellType(
            id: 4,
            birthRules: "3678",
            survivalRules: "34678",
            color: Color.FromArgb(255, 170, 120, 255),
            name: "Day & Night"
        ));

        Register(new CellType(
            id: 5,
            birthRules: "3",
            survivalRules: "12345",
            color: Color.FromArgb(255, 255, 210, 70),
            name: "Maze"
        ));

        Register(new CellType(
            id: 6,
            birthRules: "378",
            survivalRules: "235678",
            color: Color.FromArgb(255, 255, 140, 90),
            name: "Coagulations"
        ));

        Register(new CellType(
            id: 7,
            birthRules: "35678",
            survivalRules: "5678",
            color: Color.FromArgb(255, 70, 230, 200),
            name: "Diamoeba"
        ));

        Register(new CellType(
            id: 8,
            birthRules: "3",
            survivalRules: "45678",
            color: Color.FromArgb(255, 255, 70, 140),
            name: "Coral"
        ));

        Register(new CellType(
            id: 9,
            birthRules: "3678",
            survivalRules: "235678",
            color: Color.FromArgb(255, 130, 100, 255),
            name: "Stains"
        ));

        Register(new CellType(
            id: 10,
            birthRules: "35678",
            survivalRules: "5678",
            color: Color.FromArgb(255, 150, 255, 90),
            name: "Diamoeba+"
        ));

        Register(new CellType(
            id: 11,
            birthRules: "3",
            survivalRules: "12345",
            color: Color.FromArgb(255, 100, 230, 255),
            name: "Maze+"
        ));

        Register(new CellType(
            id: 12,
            birthRules: "378",
            survivalRules: "235678",
            color: Color.FromArgb(255, 255, 90, 90),
            name: "Coagulation+"
        ));
    }

    public static void Register(CellType type) => _types[type.ID] = type;
    public static int Count() => _types.Count;
    public static int CountWithoutDead() => _types.Count - 1;
    public static CellType Get(int id) => _types.TryGetValue(id, out var type) ? type : _types[0];
    public static CellType GetWithoutDead(int id) => _types.TryGetValue(id + 1, out var type) ? type : _types[1];
}
