using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators;

/// <summary>
/// Генератор по умолчанию для контролов, не имеющих явного генератора.
/// </summary>
public class DefaultElementGenerator : IElementGenerator
{
    private readonly IAttributeGenerator _attributeGenerator;
    private readonly ElementGeneratorFactory _elementGeneratorFactory;

    /// <summary>
    /// Конструктор генератора по умолчанию.
    /// </summary>
    /// <param name="attributeGenerator">Генератор атрибутов для контролов.</param>
    /// <param name="elementGeneratorFactory">Фабрика генераторов для дочерних элементов.</param>
    public DefaultElementGenerator(IAttributeGenerator attributeGenerator,
        ElementGeneratorFactory elementGeneratorFactory)
    {
        _attributeGenerator = attributeGenerator;
        _elementGeneratorFactory = elementGeneratorFactory;
    }

    /// <summary>
    /// Генерация XML-элемента для контролов без явного генератора.
    /// </summary>
    public XElement Generate(Control control)
    {
        var element = new XElement(control.GetType().Name);

        // Генерация атрибутов
        _attributeGenerator.AddAttributes(control, element);

        // Генерация дочерних элементов, если контрол имеет дочерние элементы
        if (control is Panel panel)
        {
            foreach (var child in panel.Children.OfType<Control>())
            {
                var childElementGenerator = _elementGeneratorFactory.GetGenerator(child);
                var childElement = childElementGenerator.Generate(child);
                element.Add(childElement);
            }
        }
        else if (control is ContentControl contentControl && contentControl.Content is Control contentChild)
        {
            var childElementGenerator = _elementGeneratorFactory.GetGenerator(contentChild);
            var childElement = childElementGenerator.Generate(contentChild);
            element.Add(childElement);
        }

        return element;
    }
}