using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GameOfLife.Model;
using GameOfLife.Services;

namespace GameOfLife.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Game _game;
    private readonly RenderingService _renderingService;
    private readonly DisplaySettings _displaySettings;
    private readonly TileMap _tileMap;
    private DispatcherTimer _updateTimer;
    public MainWindow()
    {
        InitializeComponent();

        int width = 800;
        int height = 800;
        _displaySettings = new DisplaySettings(width,height, 3);
        var bitmap = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgra32, null);
        Field.Source = bitmap;
        _renderingService = new RenderingService(bitmap, Field, _displaySettings);
        _tileMap = new TileMap(_displaySettings.MapSize);
        _game = new Game(_renderingService, _tileMap);
        InitializeGameLoop();
    }

    private void InitializeGameLoop()
    {
        _updateTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(10) 
        };
        _updateTimer.Tick += (_, _) =>
        {
            _game.Update();
            _renderingService.Refresh();
        };
        _updateTimer.Start();
    }
    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        _game.Start();
    }

    private void BtnPause_Click(object sender, RoutedEventArgs e)
    {
        _game.Pause();
    }

    private void BtnResume_Click(object sender, RoutedEventArgs e)
    {
        _game.Resume();
    }

    private void BtnReset_Click(object sender, RoutedEventArgs e)
    {
        _game.Reset();
    }
}