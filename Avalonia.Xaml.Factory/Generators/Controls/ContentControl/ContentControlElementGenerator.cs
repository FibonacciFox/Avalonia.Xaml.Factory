using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.ContentControl;

/// <summary>
/// Генератор элементов для ContentControl-контролов.
/// </summary>
public class ContentControlElementGenerator : IElementGenerator
{
    private readonly IAttributeGenerator _attributeGenerator;
    private readonly ElementGeneratorFactory _elementGeneratorFactory;

    /// <summary>
    /// Конструктор генератора для ContentControl.
    /// </summary>
    /// <param name="attributeGenerator">Генератор атрибутов для контрола.</param>
    /// <param name="elementGeneratorFactory">Фабрика генераторов для дочерних элементов.</param>
    public ContentControlElementGenerator(IAttributeGenerator attributeGenerator,
        ElementGeneratorFactory elementGeneratorFactory)
    {
        _attributeGenerator = attributeGenerator;
        _elementGeneratorFactory = elementGeneratorFactory;
    }

    /// <summary>
    /// Генерация XML-элемента для ContentControl.
    /// </summary>
    public XElement Generate(Control control)
    {
        var contentControl = control as Avalonia.Controls.ContentControl;
        var element = new XElement("ContentControl");

        _attributeGenerator.AddAttributes(contentControl, element);

        if (contentControl.Content is Control contentChild)
        {
            // Используем фабрику для получения правильного генератора для дочернего элемента
            var childElementGenerator = _elementGeneratorFactory.GetGenerator(contentChild);
            var childElement = childElementGenerator.Generate(contentChild);
            element.Add(childElement);
        }

        return element;
    }
}