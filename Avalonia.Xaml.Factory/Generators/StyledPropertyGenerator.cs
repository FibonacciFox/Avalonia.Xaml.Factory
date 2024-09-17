using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators;

/// <summary>
/// Генератор для StyledProperty элементов Avalonia UI, который добавляет атрибуты.
/// </summary>
public class StyledPropertyGenerator : IElementGenerator
{
    private readonly Control _control;

    public StyledPropertyGenerator(Control control)
    {
        _control = control;
    }

    /// <summary>
    /// Генерация атрибутов для StyledProperty.
    /// </summary>
    public void Generate(XamlDocumentBuilder builder)
    {
        var properties = AvaloniaPropertyRegistry.Instance.GetRegistered(_control.GetType());

        foreach (var property in properties)
        {
            if (property.GetType().IsGenericType && property.GetType().GetGenericTypeDefinition() == typeof(StyledProperty<>))
            {
                if (_control.IsSet(property))
                {
                    var value = _control.GetValue(property);
                    builder.AddAttribute(property.Name, SerializeValue(value));
                }
            }
        }
    }

    /// <summary>
    /// Метод для сериализации значений StyledProperty.
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