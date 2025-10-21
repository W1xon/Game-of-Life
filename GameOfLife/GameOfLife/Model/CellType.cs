using System.Drawing;

namespace GameOfLife.Model;
public class CellType :  ObservableObject
{
    private string _birthRules;
    private string _survivalRules;
    public int ID { get; set; }
    public string BirthRules
    {
        get => _birthRules;
        set => Set(ref _birthRules, value);
    }

    public string SurvivalRules
    {
        get => _survivalRules;
        set => Set(ref _survivalRules, value);
    }

    public Color Color { get; set; }

    public CellType(int id, string birthRules, string survivalRules, Color color)
    {
        ID = id;
        BirthRules = birthRules;
        SurvivalRules = survivalRules;
        Color = color;
    }

    public bool ShouldBeBorn(int neighbors) => BirthRules.Contains(neighbors.ToString());
    public bool ShouldSurvive(int neighbors) => SurvivalRules.Contains(neighbors.ToString());

}
