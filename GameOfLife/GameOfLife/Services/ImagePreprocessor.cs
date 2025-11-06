using System;
using System.Windows.Media.Imaging;
using OpenCvSharp;

namespace GameOfLife.Services;

public class ImagePreprocessor
{
    /// <summary>
    /// 1. Классический Canny - четкие контуры
    /// </summary>
    public WriteableBitmap CannyEdgeDetection(WriteableBitmap source, double threshold1 = 50, double threshold2 = 150)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        using var mat = source.ToMat();
        using var gray = new Mat();
        using var edges = new Mat();
        
        // Проверка формата
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);
        
        // Canny имеет встроенное размытие, но явное размытие дает лучший контроль
        using var blurred = new Mat();
        Cv2.GaussianBlur(gray, blurred, new OpenCvSharp.Size(5, 5), 1.5);
        Cv2.Canny(blurred, edges, threshold1, threshold2);
        
        return edges.ToWriteableBitmap();
    }
    
    public WriteableBitmap CannyEdgeDetection(BitmapSource source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        // Если уже WriteableBitmap - используем напрямую
        if (source is WriteableBitmap writeable)
            return CannyEdgeDetection(writeable);
        
        var tempBitmap = new WriteableBitmap(source);
        return CannyEdgeDetection(tempBitmap);
    }
    
    /// <summary>
    /// 2. Адаптивный порог - хорош для неравномерного освещения
    /// </summary>
    public WriteableBitmap AdaptiveThreshold(WriteableBitmap source, int blockSize = 11, double c = 2)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (blockSize % 2 == 0) throw new ArgumentException("blockSize должен быть нечетным", nameof(blockSize));
        
        using var mat = source.ToMat();
        using var gray = new Mat();
        using var result = new Mat();
        
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);
        
        Cv2.AdaptiveThreshold(gray, result, 255,
            AdaptiveThresholdTypes.GaussianC,
            ThresholdTypes.Binary, blockSize, c);
        
        // Инверсия для черных линий на белом
        Cv2.BitwiseNot(result, result);
        
        return result.ToWriteableBitmap();
    }
    
    public WriteableBitmap AdaptiveThreshold(BitmapSource source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        if (source is WriteableBitmap writeable)
            return AdaptiveThreshold(writeable);
        
        var tempBitmap = new WriteableBitmap(source);
        return AdaptiveThreshold(tempBitmap);
    }
    
    /// <summary>
    /// 3. Sobel - градиентные контуры
    /// </summary>
    public WriteableBitmap SobelEdges(WriteableBitmap source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        using var mat = source.ToMat();
        using var gray = new Mat();
        using var sobelX = new Mat();
        using var sobelY = new Mat();
        using var absX = new Mat();
        using var absY = new Mat();
        using var result = new Mat();
        
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);
        
        Cv2.Sobel(gray, sobelX, MatType.CV_64F, 1, 0, ksize: 3);
        Cv2.Sobel(gray, sobelY, MatType.CV_64F, 0, 1, ksize: 3);
        
        Cv2.ConvertScaleAbs(sobelX, absX);
        Cv2.ConvertScaleAbs(sobelY, absY);
        
        Cv2.AddWeighted(absX, 0.5, absY, 0.5, 0, result);
        
        return result.ToWriteableBitmap();
    }
    
    /// <summary>
    /// 4. Canny с утолщением линий 
    /// </summary>
    public WriteableBitmap ThickEdges(WriteableBitmap source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        using var mat = source.ToMat();
        using var gray = new Mat();
        using var blurred = new Mat();
        using var edges = new Mat();
        using var dilated = new Mat();
        
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);
        
        Cv2.GaussianBlur(gray, blurred, new OpenCvSharp.Size(5, 5), 1.5);
        Cv2.Canny(blurred, edges, 30, 100);
        
        // Утолщение линий
        using var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
        Cv2.Dilate(edges, dilated, kernel, iterations: 1);
        
        return dilated.ToWriteableBitmap();
    }
    
    /// <summary>
    /// 5. Лапласиан - детектирует все перепады яркости
    /// </summary>
    public WriteableBitmap LaplacianEdges(WriteableBitmap source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        using var mat = source.ToMat();
        using var gray = new Mat();
        using var blurred = new Mat();
        using var laplacian = new Mat();
        using var result = new Mat();
        
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);
        
        Cv2.GaussianBlur(gray, blurred, new OpenCvSharp.Size(5, 5), 1.5);
        Cv2.Laplacian(blurred, laplacian, MatType.CV_64F, ksize: 3);
        Cv2.ConvertScaleAbs(laplacian, result);
        
        return result.ToWriteableBitmap();
    }
    
    /// <summary>
    /// 6. Контуры объектов - только внешние границы
    /// </summary>
    public WriteableBitmap ObjectContours(WriteableBitmap source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        using var mat = source.ToMat();
        using var gray = new Mat();
        using var binary = new Mat();
        using var result = new Mat(mat.Size(), MatType.CV_8UC1, Scalar.White);
        
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);
        
        // Используем автоматический порог Otsu вместо жесткого 127
        Cv2.Threshold(gray, binary, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
        
        Cv2.FindContours(binary, out var contours, out _, 
            RetrievalModes.External, 
            ContourApproximationModes.ApproxSimple);
        
        Cv2.DrawContours(result, contours, -1, Scalar.Black, 2);
        
        return result.ToWriteableBitmap();
    }
    
    /// <summary>
    /// 7. Высококонтрастный - усиленные границы с unsharp mask
    /// </summary>
    public WriteableBitmap HighContrastEdges(WriteableBitmap source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        using var mat = source.ToMat();
        using var gray = new Mat();
        using var equalized = new Mat();
        using var sharpened = new Mat();
        using var edges = new Mat();
        
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);
        
        // Увеличение контраста через гистограммную эквализацию
        Cv2.EqualizeHist(gray, equalized);
        
        // Unsharp mask для усиления резкости
        using var blurred = new Mat();
        Cv2.GaussianBlur(equalized, blurred, new OpenCvSharp.Size(0, 0), 3);
        Cv2.AddWeighted(equalized, 1.5, blurred, -0.5, 0, sharpened);
        
        Cv2.Canny(sharpened, edges, 50, 150);
        
        return edges.ToWriteableBitmap();
    }
}