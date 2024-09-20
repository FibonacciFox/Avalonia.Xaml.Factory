using System.Xml.Linq;

namespace Avalonia.Xaml.Factory
{
    /// <summary>
    /// Билдер для создания XAML-документа. Предоставляет методы для добавления элементов и атрибутов.
    /// </summary>
    public class XamlDocumentBuilder
    {
        private XElement _currentElement;

        /// <summary>
        /// Конструктор XamlDocumentBuilder.
        /// </summary>
        public XamlDocumentBuilder()
        {
            _currentElement = null;
        }

        /// <summary>
        /// Начинает создание нового элемента XAML.
        /// </summary>
        /// <param name="elementName">Имя элемента.</param>
        public void CreateElement(string elementName)
        {
            var element = new XElement(elementName);

            if (_currentElement == null)
            {
                _currentElement = element;
            }
            else
            {
                _currentElement.Add(element);
                _currentElement = element;
            }
        }

        /// <summary>
        /// Завершает текущий элемент XAML и возвращается к родительскому элементу.
        /// </summary>
        public void EndElement()
        {
            if (_currentElement?.Parent != null)
            {
                _currentElement = _currentElement.Parent;
            }
        }

        /// <summary>
        /// Добавляет атрибут к текущему элементу XAML.
        /// </summary>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="value">Значение атрибута.</param>
        public void AddAttribute(string attributeName, string value)
        {
            if (_currentElement != null)
            {
                _currentElement.Add(new XAttribute(attributeName, value));
            }
        }

        /// <summary>
        /// Возвращает XML-представление текущего документа.
        /// </summary>
        /// <returns>Строка, представляющая XML-документ.</returns>
        public string GetXml()
        {
            return _currentElement?.ToString();
        }
    }
}
