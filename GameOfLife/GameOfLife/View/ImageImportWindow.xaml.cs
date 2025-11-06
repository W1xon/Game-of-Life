using System.Windows;
using System.Windows.Media.Imaging;
using GameOfLife.Services;

namespace GameOfLife.View;

public partial class ImageImportWindow : Window
{
    private readonly ImageProcessingService _processingService = new();
    private CancellationTokenSource? _cts;
    private WriteableBitmap? _originalSource;
    
    public WriteableBitmap? SelectedImage { get; private set; }

    public ImageImportWindow()
    {
        InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        Close();
    }

    private async void OpenButton_OnClick(object sender, RoutedEventArgs e)
    {
        SetUIState(true);
        
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            var source = ImageImportService.Import();
            if (source == null)
                return;

            _originalSource = ImageImportService.ResizeImageAuto(source);
            OriginalImg.Source = _originalSource;
            ProcessingProgress.Value = 0;

            var previewSource = ImageImportService.CreatePreview(_originalSource);
            var progress = new Progress<int>(value => ProcessingProgress.Value = value);
            var results = await _processingService.ProcessAllAsync(
                previewSource, 
                progress, 
                _cts.Token);

            DisplayResults(results);
        }
        catch (OperationCanceledException)
        {
            ProcessingProgress.Value = 0;
        }
        finally
        {
            SetUIState(false);
        }
    }

    private void DisplayResults(Dictionary<ImageProcessingService.ProcessingType, WriteableBitmap> results)
    {
        CannyImg.Source = results[ImageProcessingService.ProcessingType.Canny];
        ThresholdImg.Source = results[ImageProcessingService.ProcessingType.AdaptiveThreshold];
        SobelImg.Source = results[ImageProcessingService.ProcessingType.Sobel];
        ThickImg.Source = results[ImageProcessingService.ProcessingType.ThickEdges];
        LaplacianImg.Source = results[ImageProcessingService.ProcessingType.Laplacian];
        ContoursImg.Source = results[ImageProcessingService.ProcessingType.ObjectContours];
        HighContrastImg.Source = results[ImageProcessingService.ProcessingType.HighContrast];
    }

    private void SetUIState(bool isProcessing)
    {
        OpenButton.IsEnabled = !isProcessing;
        CloseButton.IsEnabled = !isProcessing;
    }

    private async void SelectCanny_Click(object sender, RoutedEventArgs e) =>
        await SelectProcessedImageAsync(ImageProcessingService.ProcessingType.Canny);

    private async void SelectThreshold_Click(object sender, RoutedEventArgs e) =>
        await SelectProcessedImageAsync(ImageProcessingService.ProcessingType.AdaptiveThreshold);

    private async void SelectSobel_Click(object sender, RoutedEventArgs e) =>
        await SelectProcessedImageAsync(ImageProcessingService.ProcessingType.Sobel);

    private async void SelectThick_Click(object sender, RoutedEventArgs e) =>
        await SelectProcessedImageAsync(ImageProcessingService.ProcessingType.ThickEdges);

    private async void SelectLaplacian_Click(object sender, RoutedEventArgs e) =>
        await SelectProcessedImageAsync(ImageProcessingService.ProcessingType.Laplacian);

    private async void SelectContours_Click(object sender, RoutedEventArgs e) =>
        await SelectProcessedImageAsync(ImageProcessingService.ProcessingType.ObjectContours);

    private async void SelectHighContrast_Click(object sender, RoutedEventArgs e) =>
        await SelectProcessedImageAsync(ImageProcessingService.ProcessingType.HighContrast);

    private async Task SelectProcessedImageAsync(ImageProcessingService.ProcessingType type)
    {
        if (_originalSource == null) return;

        ProcessingProgress.IsIndeterminate = true;
        SetUIState(true);

        try
        {
            SelectedImage = await _processingService.ProcessAsync(_originalSource, type);

            if (SelectedImage != null)
            {
                DialogResult = true;
                Close();
            }
        }
        finally
        {
            ProcessingProgress.IsIndeterminate = false;
            SetUIState(false);
        }
    }
}