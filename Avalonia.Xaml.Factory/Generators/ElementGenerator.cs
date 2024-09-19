using Avalonia.Controls;

namespace Avalonia.Xaml.Factory.Generators
{
    public static class ElementGenerator
    {
        public static void GenerateElementWithAttributes(Control control, XamlDocumentBuilder builder)
        {
            builder.AddElement(control.GetType().Name);
            ElementFactory.GenerateProperties(control, builder);
        }
    }
}