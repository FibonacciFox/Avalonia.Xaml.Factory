using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaXamlFactoryDemo;

public partial class MyUserControl : UserControl
{
    public MyUserControl()
    {
        InitializeComponent();
        Name = "HELLO"; 
        Background = Brushes.Gray;
        Width = 200;
        Height = 300;
        Content = "HELLO WORLD";
        VerticalAlignment = VerticalAlignment.Stretch;
        FontSize = 10;
        VerticalContentAlignment = VerticalAlignment.Stretch;
    }
}
