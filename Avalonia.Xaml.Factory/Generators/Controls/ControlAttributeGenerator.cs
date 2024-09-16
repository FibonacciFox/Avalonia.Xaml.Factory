using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls;

    /// <summary>
    /// Генератор атрибутов для всех типов контролов.
    /// Включает кэширование свойств для повышения производительности.
    /// </summary>
    public class ControlAttributeGenerator : IAttributeGenerator
    {
        private readonly Dictionary<Type, List<AvaloniaProperty>> _propertyCache = new Dictionary<Type, List<AvaloniaProperty>>();

        /// <summary>
        /// Добавляет атрибуты для указанного контрола в XML-элемент.
        /// </summary>
        /// <param name="control">Контрол для которого нужно добавить атрибуты.</param>
        /// <param name="element">XML-элемент в который добавляются атрибуты.</param>
        public void AddAttributes(Control control, XElement element)
        {
            var controlType = control.GetType();
            if (!_propertyCache.ContainsKey(controlType))
            {
                _propertyCache[controlType] = AvaloniaPropertyRegistry.Instance.GetRegistered(control).ToList();
            }

            foreach (var property in _propertyCache[controlType])
            {
                if (control is AvaloniaObject avaloniaObject && avaloniaObject.IsSet(property))
                {
                    var value = avaloniaObject.GetValue(property);
                    if (value != null && IsSimpleType(value.GetType()))
                    {
                        string formattedValue = FormatValue(value);
                        element.Add(new XAttribute(property.Name, formattedValue));
                    }
                }
            }
        }

        private bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(double) ||
                   type == typeof(float) || type == typeof(int) || type == typeof(bool) ||
                   typeof(IBrush).IsAssignableFrom(type) || type == typeof(Thickness) || type.IsEnum;
        }

        private string FormatValue(object value)
        {
            if (value is SolidColorBrush solidColorBrush)
                return solidColorBrush.Color.ToString();
            if (value is IBrush brush)
                return brush.ToString();  // Обработка кистей
            if (value is Thickness thickness)
                return $"{thickness.Left},{thickness.Top},{thickness.Right},{thickness.Bottom}";
            return value.ToString();
        }
    }

