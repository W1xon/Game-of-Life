using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace GameOfLife.View;

public partial class GameInfo : Window
{
    public GameInfo()
    {
        InitializeComponent();
    }

    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
            DragMove();
    }
    private void OpenLink(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.Tag is string url)
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }

}
