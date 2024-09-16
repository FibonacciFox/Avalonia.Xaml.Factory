using System.Xml.Linq;
using Avalonia.Controls;

namespace Avalonia.Xaml.Factory.Interfaces;

/// <summary>
/// Интерфейс для генерации атрибутов для контролов.
/// </summary>
public interface IAttributeGenerator
{
    void AddAttributes(Control control, XElement element);
}