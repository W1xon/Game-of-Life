using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cellverse.View;

    public partial class CustomMessageBox : Window
    {
        public enum MessageType
        {
            Success,
            Info,
            Warning,
            Error
        }

        public CustomMessageBox(string title, string message, MessageType type = MessageType.Info)
        {
            InitializeComponent();
            
            TitleText.Text = title;
            MessageTextBlock.Text = message;
            
            ConfigureStyle(type);
        }

        private void ConfigureStyle(MessageType type)
        {
            Color borderColor;
            Color backgroundColor;
            string icon;
            string titleIcon;
            
            switch (type)
            {
                case MessageType.Success:
                    borderColor = (Color)ColorConverter.ConvertFromString("#00D9A5"); // AccentGreen
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#00D9A5");
                    icon = "✓";
                    titleIcon = "✅";
                    break;
                    
                case MessageType.Warning:
                    borderColor = (Color)ColorConverter.ConvertFromString("#F5BA2E"); // AccentGold
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#F5BA2E");
                    icon = "⚠";
                    titleIcon = "⚠️";
                    break;
                    
                case MessageType.Error:
                    borderColor = (Color)ColorConverter.ConvertFromString("#F85149"); // AccentRed
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#F85149");
                    icon = "✕";
                    titleIcon = "❌";
                    break;
                    
                case MessageType.Info:
                default:
                    borderColor = (Color)ColorConverter.ConvertFromString("#2F81F7"); // AccentBlue
                    backgroundColor = (Color)ColorConverter.ConvertFromString("#2F81F7");
                    icon = "ℹ";
                    titleIcon = "💬";
                    break;
            }
            
            // Применяем цвета
            IconBorder.BorderBrush = new SolidColorBrush(borderColor);
            IconBorder.BorderThickness = new Thickness(2);
            IconText.Text = icon;
            IconText.Foreground = new SolidColorBrush(borderColor);
            
            TitleIconBorder.Background = new SolidColorBrush(backgroundColor);
            TitleIconText.Text = titleIcon;
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public static void Show(string title, string message, MessageType type = MessageType.Info)
        {
            var msgBox = new CustomMessageBox(title, message, type);
            msgBox.ShowDialog();
        }

        public static void ShowSuccess(string title, string message)
        {
            Show(title, message, MessageType.Success);
        }

        public static void ShowInfo(string title, string message)
        {
            Show(title, message, MessageType.Info);
        }

        public static void ShowWarning(string title, string message)
        {
            Show(title, message, MessageType.Warning);
        }

        public static void ShowError(string title, string message)
        {
            Show(title, message, MessageType.Error);
        }
    }
