using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators
{
    public class StyledPropertyGenerator : IPropertyGenerator
    {
        private readonly Control _control;

        public StyledPropertyGenerator(Control control)
        {
            _control = control;
        }

        public void Generate(XamlDocumentBuilder builder, AvaloniaProperty property, Control defaultControl = null)
        {
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

        private string SerializeValue(object value)
        {
            return value?.ToString() ?? "null";
        }
    }
}