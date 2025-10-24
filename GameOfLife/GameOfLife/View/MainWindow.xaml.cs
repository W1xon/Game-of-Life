using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GameOfLife.Model;
using GameOfLife.Services;
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
    private GameSettings _gameSettings;
    private bool _isDrawing = false;
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
        MainViewModel.Instance.DisplaySettings.UseCellRendering = true;
        MainViewModel.Instance.DisplaySettings.MapSizeChanged += size =>
        {
            _renderingService.Clear();
            _tileMap.Resize(size);
        };
        MainViewModel.Instance.MainCellType = CellTypeRegistry.Get(1);
        var bitmap = new WriteableBitmap(
            MainViewModel.Instance.DisplaySettings.Width, 
            MainViewModel.Instance.DisplaySettings.Height, 
            96, 96, 
            PixelFormats.Bgra32, 
            null
        );
        _gameSettings = new GameSettings();
    
        Field.Source = bitmap;
    
        _renderingService = new RenderingService(bitmap, MainViewModel.Instance.DisplaySettings);
        _tileMap = new TileMap(MainViewModel.Instance.DisplaySettings.MapSize);
        _game = new Game(_tileMap, _gameSettings);
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
            _renderingService.DrawField(_tileMap);
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
    private void OpenNewCellWindow_Click(object sender, RoutedEventArgs e)
    {
        var editorWindow = new NewCellTypeWindow();
        editorWindow.DataContext = CellTypeEditorViewModel.Instance;
        editorWindow.Show();
    }

    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
     {
         if (_updateTimer != null)
         {
             _updateTimer.Interval = TimeSpan.FromMilliseconds(SpeedSlider.Value);
         }
     }

    private void Field_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _isDrawing = true;
        DrawAtPosition(sender, e);
    }
    
    private void Field_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing && e.LeftButton == MouseButtonState.Pressed)
        {
            DrawAtPosition(sender, e);
        }
    }
    
    private void Field_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _game.ResumeBeforeDraw();
        _isDrawing = false;
    }
    
    private void DrawAtPosition(object sender, MouseEventArgs e)
    {
        var image = sender as Image;
        if (image?.Source == null) return;
    
        // Получаем позицию клика относительно контрола Image
        var clickPosition = e.GetPosition(image);
        
        // Получаем реальные размеры bitmap
        var bitmapWidth = image.Source.Width;
        var bitmapHeight = image.Source.Height;
        
        // Получаем размеры контрола Image
        var controlWidth = image.ActualWidth;
        var controlHeight = image.ActualHeight;
        
        // Вычисляем масштаб (Stretch="Uniform" сохраняет пропорции)
        var scaleX = controlWidth / bitmapWidth;
        var scaleY = controlHeight / bitmapHeight;
        var scale = Math.Min(scaleX, scaleY); // Uniform использует минимальный масштаб
        
        // Вычисляем реальные размеры отображаемого изображения
        var displayedWidth = bitmapWidth * scale;
        var displayedHeight = bitmapHeight * scale;
        
        // Вычисляем смещение (изображение центрируется)
        var offsetX = (controlWidth - displayedWidth) / 2;
        var offsetY = (controlHeight - displayedHeight) / 2;
        
        // Корректируем позицию клика
        var adjustedX = clickPosition.X - offsetX;
        var adjustedY = clickPosition.Y - offsetY;
        
        // Проверяем, что клик был внутри изображения
        if (adjustedX < 0 || adjustedX > displayedWidth || 
            adjustedY < 0 || adjustedY > displayedHeight)
        {
            return; // Клик вне изображения
        }
        
        // Преобразуем в координаты bitmap
        var bitmapX = adjustedX / scale;
        var bitmapY = adjustedY / scale;
        
        // Вычисляем координаты клетки
        int cellSize = MainViewModel.Instance.DisplaySettings.CellSize;
        int cellX = (int)(bitmapX / cellSize);
        int cellY = (int)(bitmapY / cellSize);
        
        _game.PauseAfterDraw();
        GameSettings.DrawPosition = new Model.Vector(cellX, cellY);
    }
}