using Avalonia.Controls;

namespace Avalonia.Xaml.Factory.Interfaces
{
    /// <summary>
    /// Интерфейс для фабрики генераторов свойств.
    /// Определяет метод для создания генератора свойств на основе типа свойства Avalonia.
    /// </summary>
    public interface IPropertyGeneratorFactory
    {
        /// <summary>
        /// Создает генератор для заданного свойства.
        /// </summary>
        /// <param name="property">Свойство Avalonia для которого требуется генератор.</param>
        /// <param name="control">Контрол, для которого генерируется XAML.</param>
        /// <returns>Экземпляр генератора, реализующий IPropertyGenerator.</returns>
        IPropertyGenerator CreateGenerator(AvaloniaProperty property, Control control);
    }
}