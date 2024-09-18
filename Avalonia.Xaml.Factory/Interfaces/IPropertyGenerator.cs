using Avalonia.Controls;

namespace Avalonia.Xaml.Factory.Interfaces
{
    /// <summary>
    /// Интерфейс для генераторов свойств элементов Avalonia UI.
    /// </summary>
    public interface IPropertyGenerator
    {
        /// <summary>
        /// Генерирует XAML представление для свойства.
        /// </summary>
        /// <param name="builder">Билдер для XAML-документа.</param>
        /// <param name="property">Свойство Avalonia.</param>
        /// <param name="defaultControl">Контрол для сравнения значений по умолчанию (может быть null).</param>
        void Generate(XamlDocumentBuilder builder, AvaloniaProperty property, Control defaultControl = null);
    }
}