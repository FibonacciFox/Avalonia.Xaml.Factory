using System;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators
{
    /// <summary>
    /// Генератор для DirectProperty элементов Avalonia UI, который добавляет атрибуты.
    /// </summary>
    public class DirectPropertyGenerator : IProprtyGenerator
    {
        private readonly Control _control;

        public DirectPropertyGenerator(Control control)
        {
            _control = control;
        }

        /// <summary>
        /// Генерация атрибутов для DirectProperty.
        /// </summary>
        public void Generate(XamlDocumentBuilder builder)
        {
            var properties = AvaloniaPropertyRegistry.Instance.GetRegistered(_control.GetType());

            var parentType = _control.GetType().BaseType;
            if (parentType == null)
            {
                return;
            }

            var defaultControl = Activator.CreateInstance(parentType) as Control;
    
            foreach (var property in properties)
            {
                if (property.IsDirect)
                {
                    var value = _control.GetValue(property);
                    var defaultValue = defaultControl.GetValue(property);
                    
                    // Проверка на unset и null
                    if (value != AvaloniaProperty.UnsetValue && value != null)
                    {
                        if (!Equals(value, defaultValue))
                        {
                            builder.AddAttribute(property.Name, SerializeValue(value));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод для сериализации значений DirectProperty.
        /// </summary>
        private string SerializeValue(object value)
        {
            return value?.ToString() ?? "null";
        }
    }
}
