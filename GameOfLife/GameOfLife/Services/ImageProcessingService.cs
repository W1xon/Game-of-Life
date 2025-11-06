using System.Windows.Media.Imaging;

namespace GameOfLife.Services;

public class ImageProcessingService
{
    private readonly ImagePreprocessor _preprocessor = new();

    public enum ProcessingType
    {
        Canny,
        AdaptiveThreshold,
        Sobel,
        ThickEdges,
        Laplacian,
        ObjectContours,
        HighContrast
    }

    public record ProcessingTask(
        ProcessingType Type,
        string Name,
        Func<WriteableBitmap, WriteableBitmap> Processor
    );

    public List<ProcessingTask> GetAllProcessingTasks()
    {
        return new List<ProcessingTask>
        {
            new(ProcessingType.Canny, "Canny Edge Detection", _preprocessor.CannyEdgeDetection),
            new(ProcessingType.AdaptiveThreshold, "Adaptive Threshold", _preprocessor.AdaptiveThreshold),
            new(ProcessingType.Sobel, "Sobel Edges", _preprocessor.SobelEdges),
            new(ProcessingType.ThickEdges, "Thick Edges", _preprocessor.ThickEdges),
            new(ProcessingType.Laplacian, "Laplacian Edges", _preprocessor.LaplacianEdges),
            new(ProcessingType.ObjectContours, "Object Contours", _preprocessor.ObjectContours),
            new(ProcessingType.HighContrast, "High Contrast Edges", _preprocessor.HighContrastEdges)
        };
    }

    public async Task<Dictionary<ProcessingType, WriteableBitmap>> ProcessAllAsync(
        WriteableBitmap source,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var tasks = GetAllProcessingTasks();
        var results = new Dictionary<ProcessingType, WriteableBitmap>();
        int processed = 0;

        source.Freeze();

        await Task.Run(() =>
        {
            Parallel.ForEach(tasks,
                new ParallelOptions { CancellationToken = cancellationToken },
                task =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    
                    var result = task.Processor(source);
                    
                    lock (results)
                    {
                        results[task.Type] = result;
                    }

                    var currentProcessed = Interlocked.Increment(ref processed);
                    progress?.Report(currentProcessed * 100 / tasks.Count);
                });
        }, cancellationToken);

        return results;
    }

    public async Task<WriteableBitmap> ProcessAsync(
        WriteableBitmap source,
        ProcessingType type,
        CancellationToken cancellationToken = default)
    {
        var task = GetAllProcessingTasks().First(t => t.Type == type);
        
        source.Freeze();
        
        return await Task.Run(() => task.Processor(source), cancellationToken);
    }

    public WriteableBitmap Process(WriteableBitmap source, ProcessingType type)
    {
        var task = GetAllProcessingTasks().First(t => t.Type == type);
        return task.Processor(source);
    }
}