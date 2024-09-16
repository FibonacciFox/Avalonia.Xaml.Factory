using System.Xml.Linq;

namespace Avalonia.Xaml.Factory.Generators.Controls.Grid;

/// <summary>
/// Генератор для создания ColumnDefinitions и RowDefinitions для Grid-контролов.
/// </summary>
public class GridDefinitionGenerator
{
    /// <summary>
    /// Генерация сеточных атрибутов для Grid (колонки и строки).
    /// </summary>
    /// <param name="grid">Grid-контрол для которого генерируются атрибуты.</param>
    /// <param name="element">XML-элемент для добавления атрибутов.</param>
    public void Generate(Avalonia.Controls.Grid grid, XElement element)
    {
        GenerateColumnDefinitions(grid, element);
        GenerateRowDefinitions(grid, element);
    }

    private void GenerateColumnDefinitions(Avalonia.Controls.Grid grid, XElement element)
    {
        if (grid.ColumnDefinitions.Count > 0)
        {
            var columnDefinitionsElement = new XElement("Grid.ColumnDefinitions");
            foreach (var column in grid.ColumnDefinitions)
            {
                var width = column.Width.ToString();
                if (width == "1*") width = "*"; // Исправляем "1*" на "*"
                columnDefinitionsElement.Add(new XElement("ColumnDefinition", new XAttribute("Width", width)));
            }

            element.Add(columnDefinitionsElement);
        }
    }

    private void GenerateRowDefinitions(Avalonia.Controls.Grid grid, XElement element)
    {
        if (grid.RowDefinitions.Count > 0)
        {
            var rowDefinitionsElement = new XElement("Grid.RowDefinitions");
            foreach (var row in grid.RowDefinitions)
            {
                var height = row.Height.ToString();
                if (height == "1*") height = "*"; // Исправляем "1*" на "*"
                rowDefinitionsElement.Add(new XElement("RowDefinition", new XAttribute("Height", height)));
            }

            element.Add(rowDefinitionsElement);
        }
    }
}