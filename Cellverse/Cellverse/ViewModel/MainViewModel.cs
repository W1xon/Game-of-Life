using Cellverse.Model;

namespace Cellverse.ViewModel;

public class MainViewModel : ObservableObject
{
    private static MainViewModel _instance;
    private DisplaySettings _displaySettings;
    private bool _useAdditionalColors;
    private CellType _mainCellType;

    public static MainViewModel Instance
    {
        get
        {
            _instance ??= new MainViewModel();
            return _instance;
        }
    }

    public CellType MainCellType
    {
        get => _mainCellType;
        set => Set(ref _mainCellType, value);
    }

    public DisplaySettings DisplaySettings
    {
        get => _displaySettings;
        set => Set(ref _displaySettings, value);
    }

    public BrushesRegistry BrushesRegistry { get; }

    public bool UseAdditionalColors
    {
        get => _useAdditionalColors;
        set
        {
            DisplaySettings.CurrentColorStrategyName = "Base";
            Set(ref _useAdditionalColors, value);
        }
    }

    private MainViewModel()
    {
        BrushesRegistry = new BrushesRegistry();
    }
}