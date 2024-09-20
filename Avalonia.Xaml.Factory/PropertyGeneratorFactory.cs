using System;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Generators;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory
{
    /// <summary>
    /// Реализация фабрики генераторов свойств.
    /// Выбирает подходящий генератор в зависимости от типа свойства (Styled, Direct, Attached).
    /// </summary>
    public class PropertyGeneratorFactory : IPropertyGeneratorFactory
    {
        /// <summary>
        /// Создает генератор на основе типа свойства.
        /// </summary>
        /// <param name="property">Свойство Avalonia.</param>
        /// <param name="control">Контрол, для которого генерируется XAML.</param>
        /// <returns>Соответствующий генератор для свойства.</returns>
        public IPropertyGenerator CreateGenerator(AvaloniaProperty property, Control control)
        {
            if (property.IsDirect)
            {
                Console.WriteLine($"DirectProperty найдено: {property.OwnerType.Name}.{property.Name}");
                return new DirectPropertyGenerator(control);
            }

            if (property.IsAttached)
            {
                Console.WriteLine($"AttachedProperty найдено: {property.OwnerType.Name}.{property.Name}");
                return new AttachedPropertyGenerator(control);
            }
            Console.WriteLine($"StyledProperty найдено: {property.OwnerType.Name}.{property.Name}");
            return new StyledPropertyGenerator(control);
        }
    }
}