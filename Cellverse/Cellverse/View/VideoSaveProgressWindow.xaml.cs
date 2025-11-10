using System.Windows;

namespace Cellverse.View;

public partial class VideoSaveProgressWindow : Window
{
    public VideoSaveProgressWindow()
    {
        InitializeComponent();
    }

    public void SetFrameCount(int frameCount)
    {
        StatusText.Text = $"Сохранение {frameCount} кадров...\nПожалуйста, подождите.";
    }
}