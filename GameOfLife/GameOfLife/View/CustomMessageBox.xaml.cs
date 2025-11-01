using System.Windows;
using System.Windows.Media;

namespace GameOfLife.View;

public enum MessageBoxType
{
    Success,
    Error,
    Warning,
    Info
}

public partial class CustomMessageBox : Window
{
    public string MessageTitle { get; set; } = "Сообщение";
    public string MessageText { get; set; } = "";
    public MessageBoxType MessageType { get; set; } = MessageBoxType.Info;

    public CustomMessageBox()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        TitleText.Text = MessageTitle;
        MessageTextBlock.Text = MessageText;

        switch (MessageType)
        {
            case MessageBoxType.Success:
                IconText.Text = "✓";
                IconText.Foreground = new SolidColorBrush(Color.FromRgb(34, 197, 94)); // Зеленый
                OkButton.Style = (Style)FindResource("BtnGreen");
                break;
            
            case MessageBoxType.Error:
                IconText.Text = "✕";
                IconText.Foreground = new SolidColorBrush(Color.FromRgb(239, 68, 68)); // Красный
                OkButton.Style = (Style)FindResource("BtnRed");
                break;
            
            case MessageBoxType.Warning:
                IconText.Text = "⚠";
                IconText.Foreground = new SolidColorBrush(Color.FromRgb(234, 179, 8)); // Желтый
                OkButton.Style = (Style)FindResource("BtnGold");
                break;
            
            case MessageBoxType.Info:
                IconText.Text = "ℹ";
                IconText.Foreground = new SolidColorBrush(Color.FromRgb(59, 130, 246)); // Синий
                OkButton.Style = (Style)FindResource("BtnBlue");
                break;
        }
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }
}