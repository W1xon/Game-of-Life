using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace GameOfLife.Services;

public static class BitmapConversionExtensions
{
    /// <summary>
    /// Конвертация WriteableBitmap → Mat
    /// </summary>
    public static Mat ToMat(this WriteableBitmap bitmap)
    {
        using var bmp = bitmap.ToSystemDrawingBitmap();
        return BitmapConverter.ToMat(bmp);
    }

    /// <summary>
    /// Конвертация Mat → WriteableBitmap
    /// </summary>
    public static WriteableBitmap ToWriteableBitmap(this Mat mat)
    {
        using var bmp = BitmapConverter.ToBitmap(mat);
        using var ms = new MemoryStream();
        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        ms.Position = 0;

        var decoder = new PngBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
        var frame = decoder.Frames[0];
        var wb = new WriteableBitmap(frame);
        wb.Freeze();
        return wb;
    }

    /// <summary>
    /// Преобразует WriteableBitmap в System.Drawing.Bitmap
    /// </summary>
    public static Bitmap ToSystemDrawingBitmap(this WriteableBitmap wb)
    {
        using var ms = new MemoryStream();
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(wb));
        encoder.Save(ms);
        ms.Position = 0;
        return new Bitmap(ms);
    }
}