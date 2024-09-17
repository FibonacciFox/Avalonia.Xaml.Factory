using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;
using Avalonia.Xaml.Factory.Generators;

namespace Avalonia.Xaml.Factory
{
    public static class ElementFactory
    {
        public static IElementGenerator CreateGenerator(Control control)
        {
            var properties = AvaloniaPropertyRegistry.Instance.GetRegistered(control.GetType());

            foreach (var property in properties)
            {
                // Логика выбора генератора на основе типа свойства
                if (property.GetType().IsGenericType && property.GetType().GetGenericTypeDefinition() == typeof(StyledProperty<>))
                {
                    return new StyledPropertyGenerator(control);
                }

                if (property.GetType().IsGenericType && property.GetType().GetGenericTypeDefinition() == typeof(DirectProperty<,>))
                {
                    return new DirectPropertyGenerator(control);
                }
            }
            
            // Fallback если свойство не является StyledProperty или DirectProperty
            return null;
        }
    }
}