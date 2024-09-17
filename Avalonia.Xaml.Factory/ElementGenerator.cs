using Avalonia.Controls;
using Avalonia.Xaml.Factory.Generators;

namespace Avalonia.Xaml.Factory;

public static class ElementGenerator
{
    public static void GenerateElementWithAttributes(Control control, XamlDocumentBuilder builder)
    {
        // Добавляем элемент на основе типа контрола
        builder.AddElement(control.GetType().Name);

        // Генерируем атрибуты для StyledProperty
        var styledPropertyGenerator = new StyledPropertyGenerator(control);
        styledPropertyGenerator.Generate(builder);

        // Генерируем атрибуты для DirectProperty
        var directPropertyGenerator = new DirectPropertyGenerator(control);
        directPropertyGenerator.Generate(builder);
    }
}