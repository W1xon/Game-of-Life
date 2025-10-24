using System.Drawing;

namespace GameOfLife.Model;

public class GameSettings
{
    public static Vector DrawPosition { get; set; }

    public int GetNextCellState(int currentCellId, KeyValuePair<int, int> neighbourCountAndDominantType)
    {
        int neighbourCount = neighbourCountAndDominantType.Key;
        int dominantCellIndex = neighbourCountAndDominantType.Value;

        var dominantCell = CellTypeRegistry.GetWithoutDead(dominantCellIndex) ;

        // рождение клетки
        if (currentCellId == 0 && dominantCell.ShouldBeBorn(neighbourCount))
            return dominantCell.ID;

        // клетка погибает
        if (currentCellId != 0 && !dominantCell.ShouldSurvive(neighbourCount))
            return 0;

        // клетка продолжает жить
        return currentCellId == 0 ? 0 : dominantCell.ID;
    }
}