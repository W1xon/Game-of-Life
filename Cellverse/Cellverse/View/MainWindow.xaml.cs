using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Cellverse.Model;
using Cellverse.Services;
using Cellverse.ViewModel;

namespace Cellverse.View;

public partial class MainWindow : Window
{
    
    private Game _game;
    private bool _isDrawing = false;
    private readonly VideoRecordingManager _videoManager;
    private DispatcherTimer _statsTimer;
    public MainWindow()
    {
        InitializeComponent();
        
        InitializeFpsUpdater();
        DataContext = MainViewModel.Instance;

        _videoManager = new VideoRecordingManager();
    
        BtnRecord.IsEnabled = false;
        BtnRecord.Content = "⏺  Загрузка FFmpeg...";
    
        GameBorder.Loaded += OnGameAreaLoaded;
        Loaded += OnWindowLoaded;
    }
    private void InitializeFpsUpdater()
    {
        _statsTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100) 
        };
        _statsTimer.Tick += (s, e) => 
        {
            if (_game != null)
            {
                GpsText.Text = $"{_game.GPS:F1}";
                CycleTimeText.Text = $"{_game.LastCycleTimeMs:F2} мс";
                ExecutionTimeText.Text = $"{_game.LastUpdateTimeMs:F2} мс";
                ChangedCellsText.Text = $"{_game.UpdatedCellCount} шт";
                SizeFieldText.Text = $"{_game.TileMap.Size.ToString()}";
            }
        };
        _statsTimer.Start();
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
            CustomMessageBox.Show("Ошибка", "Не удалось получить размер игровой области", CustomMessageBox.MessageType.Error);
            
            return;
        }

        InitializeGameField(width, height);
        
    }

    
    private void InitializeGameField(int width, int height)
    {
        var bitmap = new WriteableBitmap(
            width,
            height,
            96, 96,
            PixelFormats.Bgra32,
            null
        );
        InitGame();
       _game.InitializeGameField(bitmap, Field, _videoManager);
    }

    private void InitGame()
    {
        _game = new Game();
    }
    
    
    private void BtnStart_Click(object sender, RoutedEventArgs e)
    {
        _game.GameController.Start();
    }
    
    private void BtnPause_Click(object sender, RoutedEventArgs e)
    {
        _game.GameController.Pause();
    }
    
    private void BtnResume_Click(object sender, RoutedEventArgs e)
    {
        _game.GameController.Resume();
    }
    
    private void BtnReset_Click(object sender, RoutedEventArgs e)
    {
        _game.GameController.Reset();
    }
    
    private async void BtnRecord_Click(object sender, RoutedEventArgs e)
    {
        if (!_videoManager.IsFFmpegReady)
        {
            CustomMessageBox.Show("Ошибка", "FFmpeg не загружен. Видеозапись недоступна.", CustomMessageBox.MessageType.Error);

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
        editorWindow.UpdateSelectedCellType += () => { _game.RedrawFull(); };
        editorWindow.ShowDialog();
    }
    
    private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if(_game != null)
         _game.UpdateTime = TimeSpan.FromMilliseconds(SpeedSlider.Value);
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
        _game.GameController.AfterDrawing();
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
        
        _game.GameController.BeforeDrawing();
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
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
    
    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
        else
        {
            WindowState = WindowState.Maximized;
        }
    }
    
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }
    private void OpenButton_OnClick(object sender, RoutedEventArgs e)
    {
        var importWindow = new ImageImportWindow();
        bool? result = importWindow.ShowDialog();

        if (result != true || importWindow.SelectedImage is null)
            return;

        InitGame();

        var clonedImage = importWindow.SelectedImage.Clone();
        _game.InitializeGameField(clonedImage, Field, _videoManager, false);
    }

    private void ThemeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _game?.RedrawFull();
    }
    private void ColorPicker_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is Border border && border.Tag is CellType cellType)
        {
            
            Color currentColor = cellType.Color;
        
            var colorPicker = new ColorPickerWindow(currentColor)
            {
                Owner = this
            };
        
            if (colorPicker.ShowDialog() == true)
            {
                cellType.Color = colorPicker.SelectedColor;
                _game?.RedrawFull();
            }
        }
    }
}