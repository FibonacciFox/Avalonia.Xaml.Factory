using System.Xml.Linq;
using Avalonia.Controls;

namespace Avalonia.Xaml.Factory.Interfaces;

/// <summary>
/// Интерфейс для генерации XML-элементов для контролов.
/// </summary>
public interface IElementGenerator
{
    XElement Generate(Control control);
}