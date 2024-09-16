using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.Separator;

/// <summary>
/// Генератор для Separator-контрола.
/// </summary>
public class SeparatorElementGenerator : IElementGenerator
{
    private readonly IAttributeGenerator _attributeGenerator;

    /// <summary>
    /// Конструктор генератора для Separator.
    /// </summary>
    public SeparatorElementGenerator(IAttributeGenerator attributeGenerator)
    {
        _attributeGenerator = attributeGenerator;
    }

    /// <summary>
    /// Генерация XML-элемента для Separator.
    /// </summary>
    public XElement Generate(Control control)
    {
        var separator = control as Avalonia.Controls.Separator;
        var element = new XElement("Separator");

        // Генерация атрибутов для Separator (если есть необходимость)
        _attributeGenerator.AddAttributes(separator, element);

        return element;
    }
}