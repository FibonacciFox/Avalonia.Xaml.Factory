using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.Grid;

/// <summary>
/// Генератор элементов для Grid-контролов.
/// </summary>
public class GridElementGenerator : IElementGenerator
{
    private readonly GridDefinitionGenerator _gridDefinitionGenerator;
    private readonly IAttributeGenerator _attributeGenerator;
    private readonly ElementGeneratorFactory _elementGeneratorFactory;

    /// <summary>
    /// Конструктор генератора для Grid.
    /// </summary>
    public GridElementGenerator(GridDefinitionGenerator gridDefinitionGenerator, IAttributeGenerator attributeGenerator,
        ElementGeneratorFactory elementGeneratorFactory)
    {
        _gridDefinitionGenerator = gridDefinitionGenerator;
        _attributeGenerator = attributeGenerator;
        _elementGeneratorFactory = elementGeneratorFactory;
    }

    /// <summary>
    /// Генерация XML-элемента для Grid-контрола.
    /// </summary>
    public XElement Generate(Control control)
    {
        var grid = control as Avalonia.Controls.Grid;
        var element = new XElement("Grid");

        // Генерация атрибутов для Grid
        _attributeGenerator.AddAttributes(grid, element);

        // Генерация ColumnDefinitions и RowDefinitions
        _gridDefinitionGenerator.Generate(grid, element);

        // Генерация дочерних элементов
        foreach (var child in grid.Children.OfType<Control>())
        {
            var childElementGenerator = _elementGeneratorFactory.GetGenerator(child);
            var childElement = childElementGenerator.Generate(child);

            // Добавление атрибутов Grid.Row и Grid.Column
            int column = Avalonia.Controls.Grid.GetColumn(child);
            childElement.Add(new XAttribute("Grid.Column", column));

            int row = Avalonia.Controls.Grid.GetRow(child);
            childElement.Add(new XAttribute("Grid.Row", row));

            // Обработка Grid.ColumnSpan и Grid.RowSpan
            int columnSpan = Avalonia.Controls.Grid.GetColumnSpan(child);
            if (columnSpan > 1)
            {
                childElement.Add(new XAttribute("Grid.ColumnSpan", columnSpan));
            }

            int rowSpan = Avalonia.Controls.Grid.GetRowSpan(child);
            if (rowSpan > 1)
            {
                childElement.Add(new XAttribute("Grid.RowSpan", rowSpan));
            }

            element.Add(childElement);
        }

        return element;
    }
}