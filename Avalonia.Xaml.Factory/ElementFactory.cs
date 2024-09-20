using System;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Generators;
using Avalonia.Xaml.Factory.Helpers;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory
{
    /// <summary>
    /// Фабрика элементов, ответственная за генерацию XAML для элементов Avalonia UI.
    /// </summary>
    public static class ElementFactory
    {
        private static readonly IPropertyGeneratorFactory GeneratorFactory = new PropertyGeneratorFactory();

        /// <summary>
        /// Генерирует все свойства для переданного контрола и добавляет их в XAML-документ через билдер.
        /// </summary>
        /// <param name="control">Контрол, для которого нужно сгенерировать свойства.</param>
        /// <param name="builder">Билдер XAML-документа.</param>
        public static void GenerateProperties(Control control, XamlDocumentBuilder builder)
        {
            var properties = AvaloniaPropertyRegistry.Instance.GetRegistered(control.GetType());
            Control defaultControl = Activator.CreateInstance(control.GetType()) as Control;

            foreach (var property in properties)
            {
                // Используем фабрику для создания нужного генератора
                var generator = GeneratorFactory.CreateGenerator(property, control);

                // Проверяем, можно ли задать это свойство, и если да — генерируем
                if (PropertyHelper.CanSetProperty(property))
                {
                    generator.Generate(builder, property, defaultControl);
                }
            }
        }
    }
}