using Avalonia.Controls;

namespace AvaloniaXamlFactoryDemo;

public partial class MyUserControl : UserControl
{
    public MyUserControl()
    {
        InitializeComponent();
        
        var listBox = new ListBox();
        listBox.Name = "ListBox1";
        listBox.SelectionMode = SelectionMode.Toggle; 
        var item1 = new ListBoxItem { Name = "Item1", Content = "Item 1" };
        var item2 = new ListBoxItem { Content = "Item 2" };
        var item3 = "Simple Text Item";
        var buttonItem = new Button { Content = "Button in ListBox" };
        
        listBox.Items.Add(item1);
        listBox.Items.Add(item2);
        listBox.Items.Add(item3);
        listBox.Items.Add(buttonItem);

        Content = listBox;
    }
}