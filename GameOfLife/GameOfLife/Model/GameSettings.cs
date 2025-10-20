using System.Drawing;

namespace GameOfLife.Model;

public class GameSettings
{ 
    public List<CellType> Cells = [new CellType(1, "3", "23", Color.Lime)];
    public int GetNextCellState(int currentCellId, KeyValuePair<int, int> neighbourCountAndDominantType)
    {
        int neighbourCount = neighbourCountAndDominantType.Key;
        int dominantCellIndex = neighbourCountAndDominantType.Value;

        var dominantCell = Cells[dominantCellIndex];

        // рождение клетки
        if (currentCellId == 0 && dominantCell.BirthRules.Contains(neighbourCount.ToString()))
            return dominantCell.ID;

        // клетка погибает
        if (currentCellId != 0 && !dominantCell.SurvivalRules.Contains(neighbourCount.ToString()))
            return 0;

        // клетка продолжает жить
        return currentCellId == 0 ? 0 : dominantCell.ID;
    }

    public  void AddCell(List<CellType> cells, string birthRules, string survivalRules, Color color)
    {
        int maxID = 0;
        foreach (var cell in cells)
        {
            if (cell.ID > maxID) maxID = cell.ID;
        }

        cells.Add(new CellType(maxID + 1, birthRules, survivalRules, color));
    }

    public  void DeleteCell(List<CellType> cells, int index)
    {
        if (index < 0 || index >= cells.Count) return;

        cells.RemoveAt(index);

        for (int i = 0; i < cells.Count; i++)
            cells[i].ID = i + 1;

    }
}