using System.Windows;

namespace GameOfLife.View
{
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
    }
}