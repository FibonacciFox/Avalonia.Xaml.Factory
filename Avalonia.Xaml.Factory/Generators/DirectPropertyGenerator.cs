using System;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators;

/// <summary>
/// Генератор для DirectProperty элементов Avalonia UI, который добавляет атрибуты.
/// </summary>
public class DirectPropertyGenerator : IElementGenerator
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
        var defaultControl = (Control)Activator.CreateInstance(typeof(Control));

        foreach (var property in properties)
        {
            if (property.IsDirect)
            {
                var value = _control.GetValue(property);
                var defaultValue = defaultControl.GetValue(property);
                if (!Equals(value, defaultValue))
                {
                    builder.AddAttribute(property.Name, SerializeValue(value));
                }
            }
        }
    }

    /// <summary>
    /// Метод для сериализации значений DirectProperty.
    /// </summary>
    private string SerializeValue(object value)
    {
        if (value == null)
        {
            return "null";
        }

        if (value is string || value is int || value is double || value is bool)
        {
            return value.ToString();
        }

        return value.ToString();
    }
}