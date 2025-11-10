using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Cellverse.Services;
using Cellverse.ViewModel;

namespace Cellverse.Model;

public class Game
{
    private RenderServiceBase _renderService;
    private GameController _gameController;
    private GameSettings _gameSettings;
    private TileMap _tileMap;
    private DispatcherTimer _updateTimer;
    private VideoRecordingManager _videoManager;
    private Image _renderingField;
    
    //Метрики
    public double GPS => _metrics.GPS;
    public double LastUpdateTimeMs => _metrics.LastUpdateTimeMs;
    public double LastCycleTimeMs => _metrics.LastCycleTimeMs;
    public int UpdatedCellCount { get; private set; }
    private readonly PerformanceMetrics _metrics = new();

    public TimeSpan UpdateTime
    {
        set
        {
            if (_updateTimer != null)
                _updateTimer.Interval = value;
        }
    }
    
    public TileMap TileMap
    {
        get => _tileMap;
        set => _tileMap = value;
    }
    
    public GameController GameController
    {
        get => _gameController;
        set => _gameController = value;
    }
    public RenderServiceBase RenderService => _renderService;

    private void InitializeGame()
    {
        _gameSettings = new GameSettings();
        _tileMap = new TileMap(MainViewModel.Instance.DisplaySettings.MapSize);
        _gameController = new GameController(_tileMap, _gameSettings);
    }
    
    public void InitializeGameField(WriteableBitmap bitmap, Image field, VideoRecordingManager videoManager, bool isFirst = true)
    {
        if (_updateTimer != null)
            _updateTimer.Stop();
        
        _videoManager = videoManager;
        int cellSize = MainViewModel.Instance.DisplaySettings == null ? 5
            : MainViewModel.Instance.DisplaySettings.CellSize;
        
        MainViewModel.Instance.DisplaySettings = new DisplaySettings(
            bitmap.PixelWidth,
            bitmap.PixelHeight,
            cellSize: cellSize)
        {
            UseCellRendering = true
        };
        
        InitializeGame();
        TileMap.InitializeMap(bitmap);
        
        MainViewModel.Instance.DisplaySettings.MapSizeChanged += size =>
        {
            _renderService.Clear();
            TileMap.Resize(size);
        };
        
        if (isFirst)
            MainViewModel.Instance.MainCellType = CellTypeRegistry.Get(1);
        
        var renderBitmap = new WriteableBitmap(
            bitmap.PixelWidth,
            bitmap.PixelHeight,
            96, 96,
            PixelFormats.Bgra32,
            null
        );
        
        _renderService = new IncrementalRenderService(renderBitmap, MainViewModel.Instance.DisplaySettings);
        field.Source = renderBitmap;
        _renderingField = field;
        _renderService.Render(TileMap);
        InitializeGameLoop();
    }

    private void InitializeGameLoop()
    {
        _metrics.StartCycle();

        _updateTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(10)
        };

        _updateTimer.Tick += (_, _) =>
        {
            _metrics.BeginUpdate();
            GameController.Update();
            UpdatedCellCount = TileMap.ChangedCell.Count;
            _renderService.Render(TileMap);
            _metrics.EndUpdate();

            if (_videoManager.IsRecording && _renderingField.Source is WriteableBitmap bitmap)
                _videoManager.CaptureFrame(bitmap);
        };

        _updateTimer.Start();
    }

    public void RedrawFull()
    {
        _renderService?.RenderFull(_tileMap);
    }
}