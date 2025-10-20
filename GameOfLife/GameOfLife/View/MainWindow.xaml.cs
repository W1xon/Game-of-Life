using System.Windows;
using System.Windows.Media;
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
    private  Game _game;
    private  RenderingService _renderingService;
    private  DisplaySettings _displaySettings;
    private  TileMap _tileMap;
    private DispatcherTimer _updateTimer;
    public MainWindow()
    {
        InitializeComponent();
    
        GameBorder.Loaded += OnGameAreaLoaded;
    }

    private void OnGameAreaLoaded(object sender, RoutedEventArgs e)
    {
        int width = (int)GameBorder.ActualWidth ;
        int height = (int)GameBorder.ActualHeight ;
    
        if (width <= 0 || height <= 0)
        {
            MessageBox.Show("Failed to get game area size");
            return;
        }
    
        InitializeGame(width, height);
    }

    private void InitializeGame(int width, int height)
    {
        _displaySettings = new DisplaySettings(width, height, cellSize: 5);
    
        var bitmap = new WriteableBitmap(
            _displaySettings.Width, 
            _displaySettings.Height, 
            96, 96, 
            PixelFormats.Bgra32, 
            null
        );
    
        Field.Source = bitmap;
    
        _renderingService = new RenderingService(bitmap, _displaySettings);
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