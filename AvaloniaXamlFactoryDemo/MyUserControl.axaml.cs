using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaXamlFactoryDemo;

public partial class MyUserControl : UserControl
{
    public MyUserControl()
    {
        InitializeComponent();
        Width = 200;
        Background = Brushes.DarkSlateGray;
        Height = 300;
        VerticalAlignment = VerticalAlignment.Center;
        FontSize = 10;
        Foreground = Brushes.White;
        Content = "HELLO WORLD";
        HorizontalContentAlignment = HorizontalAlignment.Center;
        VerticalContentAlignment = VerticalAlignment.Center;
        Grid.SetColumn(this, 4);
    }
}
