using System.Drawing;

namespace GameOfLife.Model
{
    public class CellType
    {
        public int ID { get; set; }
        public string BirthRules { get; set; }   
        public string SurvivalRules { get; set; } 

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
}