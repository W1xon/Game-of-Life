using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GameOfLife.Model;
using GameOfLife.Services;
using GameOfLife.Services.ColorStrategies;
using GameOfLife.ViewModel;

namespace GameOfLife.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private  Game _game;
    private  RenderingService _renderingService;
    private  TileMap _tileMap;
    private DispatcherTimer _updateTimer;
    private Dictionary<int, IColorStrategy> _colorStrategies=  new Dictionary<int, IColorStrategy>
        {
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
    
    public MainWindow()
    {
        InitializeComponent();
    
        DataContext = MainViewModel.Instance;
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
        MainViewModel.Instance.DisplaySettings = new DisplaySettings(width, height, cellSize: 5);
        MainViewModel.Instance.DisplaySettings.MapSizeChanged += size =>
        {
            _renderingService.Clear();
            _tileMap.Resize(size);
        };

        var bitmap = new WriteableBitmap(
            MainViewModel.Instance.DisplaySettings.Width, 
            MainViewModel.Instance.DisplaySettings.Height, 
            96, 96, 
            PixelFormats.Bgra32, 
            null
        );
    
        Field.Source = bitmap;
    
        _renderingService = new RenderingService(bitmap, MainViewModel.Instance.DisplaySettings);
        _tileMap = new TileMap(MainViewModel.Instance.DisplaySettings.MapSize);
        _game = new Game( _tileMap);
    
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
            _renderingService.DrawField(_tileMap, new PixelWaveStrategy());
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