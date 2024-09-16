using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.Menu;

public class MenuElementGenerator : IElementGenerator
{
    private readonly IAttributeGenerator _attributeGenerator;
    private readonly ElementGeneratorFactory _elementGeneratorFactory;

    public MenuElementGenerator(IAttributeGenerator attributeGenerator, ElementGeneratorFactory elementGeneratorFactory)
    {
        _attributeGenerator = attributeGenerator;
        _elementGeneratorFactory = elementGeneratorFactory;
    }

    public XElement Generate(Control control)
    {
        var menu = control as Avalonia.Controls.Menu;
        var element = new XElement("Menu");

        // Генерация атрибутов для Menu
        _attributeGenerator.AddAttributes(menu, element);

        // Генерация элементов (MenuItem, Separator)
        foreach (var item in menu.Items)
        {
            if (item is Control controlItem)
            {
                var itemGenerator = _elementGeneratorFactory.GetGenerator(controlItem);
                var itemElement = itemGenerator.Generate(controlItem);
                element.Add(itemElement);
            }
        }

        return element;
    }
}