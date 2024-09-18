using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators
{
    public class AttachedPropertyGenerator : IPropertyGenerator
    {
        private readonly Control _control;

        public AttachedPropertyGenerator(Control control)
        {
            _control = control;
        }

        public void Generate(XamlDocumentBuilder builder, AvaloniaProperty property, Control defaultControl)
        {
            if (property.IsAttached)
            {
                var value = _control.GetValue(property);
                var defaultValue = defaultControl?.GetValue(property);

                // Исключаем ненужные служебные свойства, такие как "NameScope"
                if (property.OwnerType.Name == "NameScope")
                {
                    return; // Пропускаем служебные свойства
                }

                if (value != AvaloniaProperty.UnsetValue && value != null && !Equals(value, defaultValue))
                {
                    builder.AddAttribute($"{property.OwnerType.Name}.{property.Name}", SerializeValue(value));
                }
            }
        }

        private string SerializeValue(object value)
        {
            return value?.ToString() ?? "null";
        }
    }
}