using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators
{
    /// <summary>
    /// Генератор для StyledProperty. Отвечает за генерацию XAML для свойств, унаследованных от AvaloniaProperty.
    /// </summary>
    public class StyledPropertyGenerator : IPropertyGenerator
    {
        private readonly Control _control;

        /// <summary>
        /// Конструктор для генератора StyledProperty.
        /// </summary>
        /// <param name="control">Контрол, для которого генерируется XAML.</param>
        public StyledPropertyGenerator(Control control)
        {
            _control = control;
        }

        /// <summary>
        /// Генерирует XAML для StyledProperty.
        /// </summary>
        /// <param name="builder">Билдер для создания XAML-документа.</param>
        /// <param name="property">Свойство, для которого генерируется XAML.</param>
        /// <param name="defaultControl">Контрол для сравнения значений по умолчанию.</param>
        public void Generate(XamlDocumentBuilder builder, AvaloniaProperty property, Control defaultControl = null)
        {
            // Проверяем, можно ли задать значение для свойства
            if (_control.IsSet(property))
            {
                var value = _control.GetValue(property);
                var defaultValue = defaultControl?.GetValue(property);
                
                // Пропускаем, если свойство содержит контрол
                if (value is Control)
                {
                    return;
                }

                // Добавляем проверку на корректное значение
                if (value != AvaloniaProperty.UnsetValue && value != null && !Equals(value, defaultValue))
                {
                    builder.AddAttribute(property.Name, SerializeValue(value));
                }
            }
        }

        /// <summary>
        /// Сериализует значение свойства в строку.
        /// </summary>
        /// <param name="value">Значение свойства.</param>
        /// <returns>Строковое представление значения.</returns>
        private string SerializeValue(object value)
        {
            return value?.ToString() ?? "null";
        }
    }
}
