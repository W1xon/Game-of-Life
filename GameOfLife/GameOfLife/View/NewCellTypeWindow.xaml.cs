using System.Windows;

namespace GameOfLife.View;

public partial class NewCellTypeWindow : Window
{
    public NewCellTypeWindow()
    {
        InitializeComponent();
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}