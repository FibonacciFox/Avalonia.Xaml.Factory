using System;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;
using Avalonia;

namespace Avalonia.Xaml.Factory.Generators
{
    /// <summary>
    /// Генератор для AttachedProperty элементов Avalonia UI, который добавляет атрибуты.
    /// </summary>
    public class AttachedPropertyGenerator : IElementGenerator
    {
        private readonly Control _control;

        public AttachedPropertyGenerator(Control control)
        {
            _control = control;
        }

        /// <summary>
        /// Генерация атрибутов для AttachedProperty.
        /// </summary>
        public void Generate(XamlDocumentBuilder builder)
        {
            var properties = AvaloniaPropertyRegistry.Instance.GetRegisteredAttached(_control.GetType());
            var defaultControl = (Control)Activator.CreateInstance(typeof(Control));
            foreach (var property in properties)
            {
                if (property.IsAttached)
                {
                    var value = _control.GetValue(property);
                    var defaultValue = defaultControl.GetValue(property);
                    if (!Equals(value, defaultValue))
                    {
                        builder.AddAttribute($"{property.OwnerType.Name}.{property.Name}", SerializeValue(value));
                    }
                }
            }
        }

        /// <summary>
        /// Метод для сериализации значений AttachedProperty.
        /// </summary>
        private string SerializeValue(object value)
        {
            return value?.ToString() ?? "null";
        }
    }
}