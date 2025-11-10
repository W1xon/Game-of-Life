using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cellverse.Model;

namespace Cellverse.View;

public partial class NewCellTypeWindow : Window
{
    public event Action UpdateSelectedCellType;
    public NewCellTypeWindow()
    {
        InitializeComponent();
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void ColorPicker_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is Border border && border.Tag is CellType cellType)
        {
            // Получаем текущий цвет клетки
            Color currentColor = cellType.Color;
        
            // Открываем ColorPicker
            var colorPicker = new ColorPickerWindow(currentColor)
            {
                Owner = this
            };
        
            if (colorPicker.ShowDialog() == true)
            {
                cellType.Color = colorPicker.SelectedColor;
                UpdateSelectedCellType?.Invoke();
            }
        }
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}