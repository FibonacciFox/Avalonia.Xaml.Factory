namespace Avalonia.Xaml.Factory.Interfaces;

/// <summary>
/// Интерфейс для генераторов элементов Avalonia UI.
/// </summary>
public interface IProprtyGenerator
{
    /// <summary>
    /// Генерирует XAML представление для элемента.
    /// </summary>
    /// <param name="builder">Билдер для XAML-документа.</param>
    void Generate(XamlDocumentBuilder builder);
}