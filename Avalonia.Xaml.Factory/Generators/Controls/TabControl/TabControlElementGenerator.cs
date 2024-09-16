using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.TabControl;

/// <summary>
/// Генератор элементов для TabControl-контролов.
/// </summary>
public class TabControlElementGenerator : IElementGenerator
{
    private readonly IAttributeGenerator _attributeGenerator;
    private readonly ElementGeneratorFactory _elementGeneratorFactory;

    /// <summary>
    /// Конструктор генератора для TabControl.
    /// </summary>
    public TabControlElementGenerator(IAttributeGenerator attributeGenerator,
        ElementGeneratorFactory elementGeneratorFactory)
    {
        _attributeGenerator = attributeGenerator;
        _elementGeneratorFactory = elementGeneratorFactory;
    }

    /// <summary>
    /// Генерация XML-элемента для TabControl.
    /// </summary>
    public XElement Generate(Control control)
    {
        var tabControl = control as Avalonia.Controls.TabControl;
        var element = new XElement("TabControl");

        // Генерация атрибутов для TabControl
        _attributeGenerator.AddAttributes(tabControl, element);

        // Генерация TabItems
        foreach (var tabItem in tabControl.Items.OfType<TabItem>())
        {
            var tabItemElement = new XElement("TabItem");

            // Генерация атрибутов для TabItem
            _attributeGenerator.AddAttributes(tabItem, tabItemElement);

            // Генерация контента TabItem
            if (tabItem.Content is Control contentControl)
            {
                var contentGenerator = _elementGeneratorFactory.GetGenerator(contentControl);
                var contentElement = contentGenerator.Generate(contentControl);
                tabItemElement.Add(contentElement);
            }

            element.Add(tabItemElement);
        }

        return element;
    }
}