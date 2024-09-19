using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Generators;

namespace Avalonia.Xaml.Factory
{
    public static class ElementFactory
    {
        public static void GenerateProperties(Control control, XamlDocumentBuilder builder)
        {
            // Получаем зарегистрированные StyledProperty  DirectProperty AttachedProperty
            var styledProperties = AvaloniaPropertyRegistry.Instance.GetRegistered(control.GetType());
            var directProperties = AvaloniaPropertyRegistry.Instance.GetRegisteredDirect(control.GetType());
            var attachedProperties = AvaloniaPropertyRegistry.Instance.GetRegisteredAttached(control.GetType());

            var parentType = control.GetType().BaseType;
            
            Control defaultControl = parentType != null ? Activator.CreateInstance(parentType) as Control : null;

            // Обрабатываем StyledProperty
            foreach (var styledProperty in styledProperties)
            {
               // Console.WriteLine($"StyledProperty найдено: {styledProperty.OwnerType.Name}.{styledProperty.Name}");
                var generator = new StyledPropertyGenerator(control);
                generator.Generate(builder, styledProperty, defaultControl);
            }

            // Обрабатываем DirectProperty
            foreach (var directProperty in directProperties)
            {
                Console.WriteLine($"DirectProperty найдено: {directProperty.OwnerType.Name}.{directProperty.Name}");
                var generator = new DirectPropertyGenerator(control);
                generator.Generate(builder, directProperty, defaultControl);
            }

            // Обрабатываем AttachedProperty отдельно
            foreach (var attachedProperty in attachedProperties)
            {
                //Console.WriteLine($"AttachedProperty найдено: {attachedProperty.OwnerType.Name}.{attachedProperty.Name}");
                var generator = new AttachedPropertyGenerator(control);
                generator.Generate(builder, attachedProperty, defaultControl);
            }
        }
    }
}