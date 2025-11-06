using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.Model;

public static class CellTypeRegistry
{
    private static readonly Dictionary<int, CellType> _types = new();

    static CellTypeRegistry()
    {
        
        Register(new CellType(
            id: 0,
            birthRules: "",
            survivalRules: "",
            color: Color.FromArgb(30, 32, 36), 
            name: "Пустая"
        ));

        
        Register(new CellType(
            id: 1,
            birthRules: "3",
            survivalRules: "23",
            color: Color.FromArgb(170, 220, 255), 
            name: "Обычная",
            isPriority: true
        ));

        
        Register(new CellType(
            id: 2,
            birthRules: "36",
            survivalRules: "23",
            color: Color.FromArgb(255, 180, 220), 
            name: "HighLife"
        ));

        
        Register(new CellType(
            id: 3,
            birthRules: "3",
            survivalRules: "012345678",
            color: Color.FromArgb(210, 255, 190), 
            name: "Бессмертная"
        ));

        
        Register(new CellType(
            id: 4,
            birthRules: "3678",
            survivalRules: "34678",
            color: Color.FromArgb(200, 190, 255), 
            name: "Day & Night"
        ));

        
        Register(new CellType(
            id: 5,
            birthRules: "3",
            survivalRules: "12345",
            color: Color.FromArgb(255, 250, 180), 
            name: "Maze"
        ));

        
        Register(new CellType(
            id: 6,
            birthRules: "378",
            survivalRules: "235678",
            color: Color.FromArgb(255, 200, 180), 
            name: "Coagulations"
        ));

        
        Register(new CellType(
            id: 7,
            birthRules: "35678",
            survivalRules: "5678",
            color: Color.FromArgb(160, 230, 210), 
            name: "Diamoeba"
        ));

        

        
        Register(new CellType(
            id: 8,
            birthRules: "3",
            survivalRules: "45678",
            color: Color.FromArgb(255, 110, 180), 
            name: "Dream Coral"
        ));

        
        Register(new CellType(
            id: 9,
            birthRules: "3678",
            survivalRules: "235678",
            color: Color.FromArgb(180, 130, 255), 
            name: "Dream Stains"
        ));

        
        Register(new CellType(
            id: 10,
            birthRules: "35678",
            survivalRules: "5678",
            color: Color.FromArgb(190, 255, 150), 
            name: "Dream Diamoeba"
        ));

        
        Register(new CellType(
            id: 11,
            birthRules: "3",
            survivalRules: "12345",
            color: Color.FromArgb(130, 230, 255), 
            name: "Dream Maze"
        ));

        
        Register(new CellType(
            id: 12,
            birthRules: "378",
            survivalRules: "235678",
            color: Color.FromArgb(255, 130, 130), 
            name: "Dream Coagulation"
        ));
    }

    public static void Register(CellType type) => _types[type.ID] = type;
    public static int Count() => _types.Count;
    public static int CountWithoutDead() => _types.Count - 1;

    public static CellType Get(int id) => _types.TryGetValue(id, out var type) ? type : _types[0];
    public static CellType GetWithoutDead(int id) => _types.TryGetValue(id + 1, out var type) ? type : _types[1];

    public static void Delete(int index) => _types.Remove(index);
}