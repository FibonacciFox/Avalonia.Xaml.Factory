using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaXamlFactoryDemo;

public partial class MyUserControl : UserControl
{
    public MyUserControl()
    {
        InitializeComponent();
        Background = Brushes.Gray;
        Width = 200;
        Height = 300;
        VerticalAlignment = VerticalAlignment.Stretch;
        FontSize = 10;
        VerticalContentAlignment = VerticalAlignment.Stretch;
        
        Grid.SetColumn(this, 4);
    }
}
