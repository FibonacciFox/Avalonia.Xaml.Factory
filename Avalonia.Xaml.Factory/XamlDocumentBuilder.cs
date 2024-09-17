using System.Xml.Linq;

namespace Avalonia.Xaml.Factory;

/// <summary>
/// Класс для построения XAML документа на основе объектов Avalonia UI.
/// Использует XDocument для управления XML структурой.
/// </summary>
public class XamlDocumentBuilder
{
    private XElement _rootElement;

    public XamlDocumentBuilder()
    {
        _rootElement = null; // Изначально нет корневого элемента
    }

    /// <summary>
    /// Добавляет новый элемент в документ.
    /// </summary>
    /// <param name="elementName">Имя элемента.</param>
    public void AddElement(string elementName)
    {
        var element = new XElement(elementName);

        if (_rootElement == null)
        {
            // Если нет корневого элемента, то это первый добавленный элемент
            _rootElement = element;
        }
        else
        {
            // Добавляем элемент как дочерний к текущему элементу
            _rootElement.Add(element);
        }
    }

    /// <summary>
    /// Добавляет атрибут к последнему добавленному элементу.
    /// </summary>
    /// <param name="attributeName">Имя атрибута.</param>
    /// <param name="value">Значение атрибута.</param>
    public void AddAttribute(string attributeName, string value)
    {
        if (_rootElement != null)
        {
            _rootElement.Add(new XAttribute(attributeName, value));
        }
    }

    /// <summary>
    /// Возвращает сгенерированный XML.
    /// </summary>
    public string GetXml()
    {
        // Проверяем, был ли создан корневой элемент
        return _rootElement != null ? _rootElement.ToString() : null;
    }
}