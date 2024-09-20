using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;
using Avalonia.Xaml.Factory.Helpers;

namespace Avalonia.Xaml.Factory.Generators
{
    /// <summary>
    /// Генератор для DirectProperty. Отвечает за генерацию XAML для прямых свойств, унаследованных от AvaloniaProperty.
    /// </summary>
    public class DirectPropertyGenerator : IPropertyGenerator
    {
        private readonly Control _control;

        /// <summary>
        /// Конструктор для генератора DirectProperty.
        /// </summary>
        /// <param name="control">Контрол, для которого генерируется XAML.</param>
        public DirectPropertyGenerator(Control control)
        {
            _control = control;
        }

        /// <summary>
        /// Генерирует XAML для DirectProperty.
        /// </summary>
        /// <param name="builder">Билдер для создания XAML-документа.</param>
        /// <param name="property">Свойство, для которого генерируется XAML.</param>
        /// <param name="defaultControl">Контрол для сравнения значений по умолчанию.</param>
        public void Generate(XamlDocumentBuilder builder, AvaloniaProperty property, Control defaultControl = null)
        {
            // Проверяем, можно ли задать значение для свойства
            if (PropertyHelper.CanSetProperty(property))
            {
                var value = _control.GetValue(property);
                var defaultValue = defaultControl?.GetValue(property);

                // Добавляем проверку на unset и null, а также сравниваем
                if (value != AvaloniaProperty.UnsetValue && value != null && !Equals(value, defaultValue))
                {
                    // Добавляем атрибут в XAML через билдер
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
