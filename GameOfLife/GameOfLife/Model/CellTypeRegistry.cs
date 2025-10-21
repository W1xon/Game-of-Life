using System.Drawing;

namespace GameOfLife.Model;

public static class CellTypeRegistry
{
    private static readonly Dictionary<int, CellType> _types = new();

    static CellTypeRegistry()
    {
        // 0 — Пустая
        Register(new CellType(
            id: 0,
            birthRules: "",
            survivalRules: "",
            color: Color.FromArgb(30, 32, 36),
            name: "Пустая"
        ));

        // 1 — Классика Conway (B3/S23)
        Register(new CellType(
            id: 1,
            birthRules: "3",
            survivalRules: "23",
            color: Color.FromArgb(0, 180, 110),
            name: "Обычная",
            isPriority: true
        ));

        // 2 — "HighLife" (B36/S23) — как Conway, но умеет "самокопироваться"
        Register(new CellType(
            id: 2,
            birthRules: "36",
            survivalRules: "23",
            color: Color.FromArgb(80, 140, 255),
            name: "HighLife"
        ));


        // 3 — "Life Without Death" (B3/S012345678) — только растёт, никогда не умирает
        Register(new CellType(
            id: 3,
            birthRules: "3",
            survivalRules: "012345678",
            color: Color.FromArgb(255, 160, 50),
            name: "Бессмертная"
        ));

        // 4 — "Day & Night" (B3678/S34678) — симметричная, устойчивая система
        Register(new CellType(
            id: 4,
            birthRules: "3678",
            survivalRules: "34678",
            color: Color.FromArgb(180, 120, 255),
            name: "Day & Night"
        ));

        // 5 — "Maze" (B3/S12345) — образует лабиринты и туннели
        Register(new CellType(
            id: 5,
            birthRules: "3",
            survivalRules: "12345",
            color: Color.FromArgb(120, 200, 255),
            name: "Maze"
        ));

        // 6 — "Coagulations" (B378/S235678) — густые массы
        Register(new CellType(
            id: 6,
            birthRules: "378",
            survivalRules: "235678",
            color: Color.FromArgb(255, 200, 90),
            name: "Coagulations"
        ));

        // 7 — "Diamoeba" (B35678/S5678) — живые амёбные формы
        Register(new CellType(
            id: 7,
            birthRules: "35678",
            survivalRules: "5678",
            color: Color.FromArgb(100, 255, 150),
            name: "Diamoeba"
        ));
    }

    public static void Register(CellType type) => _types[type.ID] = type;
    public static int Count() => _types.Count;
    public static int CountWithoutDead() => _types.Count - 1;

    public static CellType Get(int id) => _types.TryGetValue(id, out var type) ? type : _types[0];
    public static CellType GetWithoutDead(int id) => _types.TryGetValue(id + 1, out var type) ? type : _types[1];

    public static void Delete(int index) => _types.Remove(index);
}
