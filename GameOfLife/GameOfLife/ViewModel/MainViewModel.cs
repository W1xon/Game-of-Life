using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameOfLife.Model;
using GameOfLife.ViewModel;

namespace GameOfLife.ViewModel;

public class MainViewModel : ObservableObject
{
    private static MainViewModel _instance;
    private DisplaySettings _displaySettings;

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

    private MainViewModel() { }
}
