using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.MenuItem;

/// <summary>
/// Генератор для MenuItem-контролов.
/// </summary>
public class MenuItemElementGenerator : IElementGenerator
{
    private readonly IAttributeGenerator _attributeGenerator;
    private readonly ElementGeneratorFactory _elementGeneratorFactory;

    /// <summary>
    /// Конструктор генератора для MenuItem.
    /// </summary>
    public MenuItemElementGenerator(IAttributeGenerator attributeGenerator,
        ElementGeneratorFactory elementGeneratorFactory)
    {
        _attributeGenerator = attributeGenerator;
        _elementGeneratorFactory = elementGeneratorFactory;
    }

    /// <summary>
    /// Генерация XML-элемента для MenuItem-контролов.
    /// </summary>
    public XElement Generate(Control control)
    {
        var menuItem = control as Avalonia.Controls.MenuItem;
        var element = new XElement("MenuItem");

        // Генерация атрибутов для MenuItem
        _attributeGenerator.AddAttributes(menuItem, element);

        // Генерация подменю (если есть)
        if (menuItem.Items != null && menuItem.Items.OfType<object>().Any())
        {
            foreach (var item in menuItem.Items)
            {
                if (item is Control subControl)
                {
                    var subItemGenerator = _elementGeneratorFactory.GetGenerator(subControl);
                    var subItemElement = subItemGenerator.Generate(subControl);
                    element.Add(subItemElement);
                }
            }
        }

        return element;
    }
}