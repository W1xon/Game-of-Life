using System.Drawing;

namespace Cellverse.Model;

public class GameSettings
{
    public static Vector? DrawPosition { get; set; }
    public byte GetNextCellState(byte currentCellId, KeyValuePair<int, byte> neighbourCountAndDominantType)
    {
        int neighbourCount = neighbourCountAndDominantType.Key;
        byte dominantCellIndex = neighbourCountAndDominantType.Value;

        // 1. Получение объекта
        var dominantCell = CellTypeRegistry.GetWithoutDead(dominantCellIndex);
        if (currentCellId == 0)
        {
            // Только рождение
            return dominantCell.ShouldBeBorn(neighbourCount) ? dominantCell.ID : (byte)0;
        }
        else
        {
            // Выживание/Смерть. Если выжила, остается currentCellId.
            return dominantCell.ShouldSurvive(neighbourCount) ? currentCellId : (byte)0;
        }
    }
}