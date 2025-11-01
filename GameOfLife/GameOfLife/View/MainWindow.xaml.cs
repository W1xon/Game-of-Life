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

public partial class MainWindow : Window
{
    private Game _game;
    private RenderingService _renderingService;
    private TileMap _tileMap;
    private DispatcherTimer _updateTimer;
    private GameSettings _gameSettings;
    private bool _isDrawing = false;
    
    private VideoRecordingManager _videoManager;
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = MainViewModel.Instance;
        
        _videoManager = new VideoRecordingManager();
    
        // Отключаем кнопку записи до загрузки FFmpeg
        BtnRecord.IsEnabled = false;
        BtnRecord.Content = "⏺  Загрузка FFmpeg...";
    
        GameBorder.Loaded += OnGameAreaLoaded;
        Loaded += OnWindowLoaded;
    }
    
    private async void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        bool success = await _videoManager.InitializeFFmpegAsync(this);
        
        if (success)
        {
            BtnRecord.IsEnabled = true;
            BtnRecord.Content = "⏺  Запись";
        }
        else
        {
            BtnRecord.IsEnabled = false;
            BtnRecord.Content = "⏺  FFmpeg не доступен";
            BtnRecord.ToolTip = "FFmpeg не установлен. Видеозапись недоступна.";
        }
    }
    
    private void OnGameAreaLoaded(object sender, RoutedEventArgs e)
    {
        int width = (int)GameBorder.ActualWidth;
        int height = (int)GameBorder.ActualHeight;
    
        if (width <= 0 || height <= 0)
        {
            var errorDialog = new CustomMessageBox
            {
                Owner = this,
                MessageTitle = "Ошибка",
                MessageText = "Не удалось получить размер игровой области",
                MessageType = MessageBoxType.Error
            };
            errorDialog.ShowDialog();
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
            
            // Захватываем кадр во время записи
            if (_videoManager.IsRecording && Field.Source is WriteableBitmap bitmap)
            {
                _videoManager.CaptureFrame(bitmap);
            }
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
    
    private async void BtnRecord_Click(object sender, RoutedEventArgs e)
    {
        if (!_videoManager.IsFFmpegReady)
        {
            var errorDialog = new CustomMessageBox
            {
                Owner = this,
                MessageTitle = "Ошибка",
                MessageText = "FFmpeg не загружен. Видеозапись недоступна.",
                MessageType = MessageBoxType.Error
            };
            errorDialog.ShowDialog();
            return;
        }
        
        if (!_videoManager.IsRecording)
        {
            _videoManager.StartRecording(fps: 30);
            
            BtnRecord.Content = "⏹  Остановить";
            BtnRecord.Style = (Style)FindResource("BtnGreen");
        }
        else
        {
            BtnRecord.Content = "⏺  Запись";
            BtnRecord.Style = (Style)FindResource("BtnRed");
            BtnRecord.IsEnabled = false;
            
            await _videoManager.StopRecordingAsync(this);
            
            BtnRecord.IsEnabled = true;
        }
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
        _game.AfterDrawing();
        _isDrawing = false;
    }
    
    private void DrawAtPosition(object sender, MouseEventArgs e)
    {
        var image = sender as Image;
        if (image?.Source == null) return;
    
        var clickPosition = e.GetPosition(image);
        var bitmapWidth = image.Source.Width;
        var bitmapHeight = image.Source.Height;
        var controlWidth = image.ActualWidth;
        var controlHeight = image.ActualHeight;
        var scaleX = controlWidth / bitmapWidth;
        var scaleY = controlHeight / bitmapHeight;
        var scale = Math.Min(scaleX, scaleY);
        var displayedWidth = bitmapWidth * scale;
        var displayedHeight = bitmapHeight * scale;
        var offsetX = (controlWidth - displayedWidth) / 2;
        var offsetY = (controlHeight - displayedHeight) / 2;
        var adjustedX = clickPosition.X - offsetX;
        var adjustedY = clickPosition.Y - offsetY;
        
        if (adjustedX < 0 || adjustedX > displayedWidth || 
            adjustedY < 0 || adjustedY > displayedHeight)
        {
            return;
        }
        
        var bitmapX = adjustedX / scale;
        var bitmapY = adjustedY / scale;
        int cellSize = MainViewModel.Instance.DisplaySettings.CellSize;
        int cellX = (int)(bitmapX / cellSize);
        int cellY = (int)(bitmapY / cellSize);
        
        _game.BeforeDrawing();
        GameSettings.DrawPosition = new Model.Vector(cellX, cellY);
    }
    
    private void GameInfoButton_OnClick(object sender, RoutedEventArgs e)
    {
        var infoWindow = new GameInfo
        {
            Owner = this
        };
        infoWindow.ShowDialog();
    }
}