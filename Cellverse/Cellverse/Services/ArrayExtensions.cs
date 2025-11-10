namespace Cellverse.Services;
public static class ArrayExtensions
{ 
    public static int[,] CloneArray(this int[,] source)
    {
        if (source == null) return null;

        int rows = source.GetLength(0);
        int cols = source.GetLength(1);
        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
            result[i, j] = source[i, j];

        return result;
    }
}