using GameOfLife.Services.ColorStrategies;

namespace GameOfLife.Services;

public static class ColorStrategyRegistry
{
    private static readonly Dictionary<int, ColorStrategyBase> _colorStrategies =  new()
    {
        { 0, new StandartStrategy() },
        { 1, new GradientXGrayStrategy() },
        { 2, new GradientXModifiedGreenStrategy() },
        { 3, new GradientXHalfRedBlueStrategy() },
        { 4, new CoordinateProductModuloStrategy() },
        { 5, new CoordinateProductModifiedGreenStrategy() },
        { 6, new CoordinateProductHalfRedBlueStrategy() },
        { 7, new TrigonometricYStrategy() },
        { 8, new TrigonometricXStrategy() },
        { 9, new TrigonometricMixedStrategy() },
        { 10, new GradientXYBlueStrategy() },
        { 11, new GradientXYGreenStrategy() },
        { 12, new GradientXYRedStrategy() }
    };
    public static ColorStrategyBase Get(int id) => _colorStrategies.TryGetValue(id, out var type) ? type : _colorStrategies[0];

}