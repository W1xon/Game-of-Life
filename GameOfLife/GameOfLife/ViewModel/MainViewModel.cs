using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameOfLife.Model;
using GameOfLife.ViewModel;

namespace GameOfLife.ViewModel;

public class MainViewModel : ObservableObject
{
    private static MainViewModel _instance;
    private DisplaySettings _displaySettings;
    private bool _useAdditionalColors;
    public static MainViewModel Instance
    {
        get
        {
            _instance ??= new MainViewModel();
            return _instance;
        }
    }

    public DisplaySettings DisplaySettings
    {
        get => _displaySettings;
        set => Set(ref _displaySettings, value);
    }
    public bool UseAdditionalColors
    {
        get => _useAdditionalColors;
        set
        {
            DisplaySettings.CurrentColorStrategyName = "Base"; 
            Set(ref _useAdditionalColors, value);
        }
    }
    private MainViewModel() { }
}
