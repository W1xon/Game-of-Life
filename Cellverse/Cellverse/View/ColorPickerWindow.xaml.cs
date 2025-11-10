using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cellverse.View;

public partial class ColorPickerWindow : Window
{
    public Color SelectedColor { get; private set; }

    public ColorPickerWindow(Color initialColor)
    {
        InitializeComponent();

        SelectedColor = initialColor;
        RedSlider.Value = initialColor.R;
        GreenSlider.Value = initialColor.G;
        BlueSlider.Value = initialColor.B;

        UpdatePreview();
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void Color_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        if (PreviewColor == null || RedSlider == null || GreenSlider == null || BlueSlider == null)
            return;

        byte r = (byte)RedSlider.Value;
        byte g = (byte)GreenSlider.Value;
        byte b = (byte)BlueSlider.Value;

        SelectedColor = Color.FromRgb(r, g, b);
        PreviewColor.Color = SelectedColor;
        HexText.Text = $"#{r:X2}{g:X2}{b:X2}";
    }

    private void QuickColor_Click(object sender, MouseButtonEventArgs e)
    {
        // Поддержка и Ellipse, и Border
        if (sender is FrameworkElement element && element.Tag is string hexColor)
        {
            Color color = (Color)ColorConverter.ConvertFromString(hexColor);

            RedSlider.Value = color.R;
            GreenSlider.Value = color.G;
            BlueSlider.Value = color.B;

            UpdatePreview();
        }
    }

    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}