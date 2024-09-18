using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators
{
    public class DirectPropertyGenerator : IPropertyGenerator
    {
        private readonly Control _control;

        public DirectPropertyGenerator(Control control)
        {
            _control = control;
        }

        public void Generate(XamlDocumentBuilder builder, AvaloniaProperty property, Control defaultControl)
        {
            // Проверяем, установлено ли DirectProperty
            var value = _control.GetValue(property);
            var defaultValue = defaultControl?.GetValue(property);

            // Добавляем проверку на unset и null, а также сравниваем
            if (value != AvaloniaProperty.UnsetValue && value != null && !Equals(value, defaultValue))
            {
                // Добавляем атрибут в XAML через билдер
                builder.AddAttribute(property.Name, SerializeValue(value));
            }
        }

        /// <summary>
        /// Метод для сериализации значений DirectProperty.
        /// </summary>
        private string SerializeValue(object value)
        {
            // Преобразуем значение в строку
            return value?.ToString() ?? "null";
        }
    }
}