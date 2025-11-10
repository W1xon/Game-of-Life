using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Cellverse.Services;

public static class ImageImportService
{
    private const int PreviewMaxSize = 400;

    public static WriteableBitmap? Import()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Изображения (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png",
            Title = "Импорт изображения",
            FilterIndex = 1
        };

        if (dialog.ShowDialog() != true)
            return null;

        var bitmap = new BitmapImage();
        using (var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
        {
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            bitmap.Freeze();
        }

        return new WriteableBitmap(bitmap);
    }

    public static WriteableBitmap ResizeImageAuto(WriteableBitmap source)
    {
        int width = source.PixelWidth;
        float percent = CalculateResizePercent(width);

        if (Math.Abs(percent - 1f) < 0.01f)
            return source;

        return ResizeImage(source, percent);
    }

    private static float CalculateResizePercent(int width)
    {
        return width switch
        {
            < 400 => 800f / width,
            <= 1000 => 1f,
            <= 2000 => 1f,
            <= 4000 => 2000f / width,
            _ => 1500f / width
        };
    }

    public static WriteableBitmap ResizeImage(WriteableBitmap source, float percent)
    {
        int maxSize = (int)(source.PixelWidth * percent);
        return ResizeImage(source, maxSize);
    }

    public static WriteableBitmap ResizeImage(WriteableBitmap source, int maxSize)
    {
        double scale = Math.Min(
            maxSize / (double)source.PixelWidth,
            maxSize / (double)source.PixelHeight);

        var scaledBitmap = new TransformedBitmap(source,
            new ScaleTransform(scale, scale));

        var writeableBitmap = new WriteableBitmap(scaledBitmap);
        writeableBitmap.Freeze();

        return writeableBitmap;
    }

    public static WriteableBitmap CreatePreview(WriteableBitmap source)
    {
        return ResizeImage(source, PreviewMaxSize);
    }
}