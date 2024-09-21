using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators
{
    /// <summary>
    /// Генератор для AttachedProperty. Отвечает за генерацию XAML для присоединенных свойств AvaloniaProperty.
    /// </summary>
    public class AttachedPropertyGenerator : IPropertyGenerator
    {
        private readonly Control _control;

        /// <summary>
        /// Конструктор для генератора AttachedProperty.
        /// </summary>
        /// <param name="control">Контрол, для которого генерируется XAML.</param>
        public AttachedPropertyGenerator(Control control)
        {
            _control = control;
        }

        /// <summary>
        /// Генерирует XAML для AttachedProperty.
        /// </summary>
        /// <param name="builder">Билдер для создания XAML-документа.</param>
        /// <param name="property">Свойство, для которого генерируется XAML.</param>
        /// <param name="defaultControl">Контрол для сравнения значений по умолчанию.</param>
        public void Generate(XamlDocumentBuilder builder, AvaloniaProperty property, Control defaultControl = null)
        {
            if (property.IsAttached)
            {
                var value = _control.GetValue(property);
                var defaultValue = defaultControl?.GetValue(property);

                // Проверяем наличие значения и его отличие от значения по умолчанию
                if (value != AvaloniaProperty.UnsetValue && value != null && !Equals(value, defaultValue))
                {
                    // Если значение присоединенного свойства - Control, создаем сложный элемент
                    if (value is Control controlValue)
                    {
                        // Начинаем новый элемент для сложного присоединенного свойства
                        builder.CreateElement($"{property.OwnerType.Name}.{property.Name}");
                        ElementGenerator.GenerateElement(controlValue, builder);
                        builder.EndElement(); // Завершаем сложный элемент
                    }
                    else
                    {
                        // Для простых значений просто добавляем атрибут
                        builder.AddAttribute($"{property.OwnerType.Name}.{property.Name}", SerializeValue(value));
                    }
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