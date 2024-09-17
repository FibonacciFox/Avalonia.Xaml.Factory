using System.Xml.Linq;

namespace Avalonia.Xaml.Factory.Interfaces;

/// <summary>
/// Интерфейс для генераторов свойств, которые генерируют XML для свойств Avalonia.
/// </summary>
public interface IPropertyGenerator
{
    /// <summary>
    /// Генерирует атрибут XML для указанного свойства и контрола Avalonia.
    /// </summary>
    /// <param name="control">Экземпляр контрола Avalonia, у которого есть это свойство.</param>
    /// <param name="property">Свойство Avalonia, для которого нужно сгенерировать XML.</param>
    /// <returns>Атрибут XML, представляющий это свойство, или null, если свойство не установлено.</returns>
    XAttribute? Generate(AvaloniaObject control, AvaloniaProperty property);
}