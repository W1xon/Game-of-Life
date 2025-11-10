using System.IO;
using FFMpegCore;
using System.Windows.Media.Imaging;

namespace Cellverse.Services;

public class VideoRecorder
{
    private readonly List<string> _framePaths = new();
    private readonly int _fps;
    private int _width;
    private int _height;
    private string _tempFolder;
    
    public VideoRecorder(int fps = 30)
    {
        _fps = fps;
        _tempFolder = Path.Combine(Path.GetTempPath(), $"GameOfLife_{Guid.NewGuid()}");
        Directory.CreateDirectory(_tempFolder);
    }
    
    public void CaptureFrame(WriteableBitmap bitmap)
    {
        _width = bitmap.PixelWidth;
        _height = bitmap.PixelHeight;
        
        var framePath = Path.Combine(_tempFolder, $"frame_{_framePaths.Count:D6}.png");
        
        using (var fileStream = new FileStream(framePath, FileMode.Create))
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(fileStream);
        }
        
        _framePaths.Add(framePath);
    }
    
    public async Task SaveVideoAsync(string outputPath)
    {
        if (_framePaths.Count == 0)
            throw new InvalidOperationException("Нет кадров для сохранения");
    
        var inputPattern = Path.Combine(_tempFolder, "frame_%06d.png");
    
        int evenWidth = _width % 2 == 0 ? _width : _width + 1;
        int evenHeight = _height % 2 == 0 ? _height : _height + 1;
    
        await FFMpegArguments
            .FromFileInput(inputPattern, false, options => options
                .WithFramerate(_fps))
            .OutputToFile(outputPath, overwrite: true, options => options
                .WithVideoCodec("libx264")
                .ForcePixelFormat("yuv420p")
                .WithConstantRateFactor(23)
                .WithCustomArgument($"-vf scale={evenWidth}:{evenHeight}:force_original_aspect_ratio=decrease,pad={evenWidth}:{evenHeight}:(ow-iw)/2:(oh-ih)/2")
                .WithFramerate(_fps))
            .ProcessAsynchronously();
    
        Clear();
    }
    
    public void Clear()
    {
        foreach (var path in _framePaths)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch { }
        }
        
        _framePaths.Clear();
        
        try
        {
            if (Directory.Exists(_tempFolder))
                Directory.Delete(_tempFolder, true);
        }
        catch { }
        
        _tempFolder = Path.Combine(Path.GetTempPath(), $"GameOfLife_{Guid.NewGuid()}");
        Directory.CreateDirectory(_tempFolder);
    }
    
    public int FrameCount => _framePaths.Count;
    
}