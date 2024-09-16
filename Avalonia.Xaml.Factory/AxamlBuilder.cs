using System.Xml.Linq;
using Avalonia.Controls;

namespace Avalonia.Xaml.Factory;

/// <summary>
/// Основной класс для генерации AXAML документа.
/// Использует фабрику для выбора подходящего генератора в зависимости от типа контрола.
/// </summary>
public class AxamlBuilder
{
    private readonly ElementGeneratorFactory _elementGeneratorFactory;

    /// <summary>
    /// Конструктор класса AxamlGenerator.
    /// </summary>
    /// <param name="elementGeneratorFactory">Фабрика генераторов элементов для различных контролов.</param>
    public AxamlBuilder(ElementGeneratorFactory elementGeneratorFactory)
    {
        _elementGeneratorFactory = elementGeneratorFactory;
    }

    /// <summary>
    /// Генерирует AXAML-документ для заданного контрола.
    /// </summary>
    /// <param name="control">Контрол для которого генерируется AXAML.</param>
    /// <returns>XDocument с AXAML представлением контрола.</returns>
    public XDocument Build(Control control)
    {
        var elementGenerator = _elementGeneratorFactory.GetGenerator(control);
        var rootElement = elementGenerator.Generate(control);
        return new XDocument(rootElement);
    }
}