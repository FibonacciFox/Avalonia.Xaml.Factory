using System.Xml.Linq;

namespace Avalonia.Xaml.Factory
{
    public class XamlDocumentBuilder
    {
        private XElement _currentElement;

        public XamlDocumentBuilder()
        {
            _currentElement = null;
        }

        // Начало нового элемента
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

        // Завершение элемента (возвращаемся к родительскому элементу)
        public void EndElement()
        {
            if (_currentElement?.Parent != null)
            {
                _currentElement = _currentElement.Parent;
            }
        }

        // Добавление атрибутов
        public void AddAttribute(string attributeName, string value)
        {
            if (_currentElement != null)
            {
                _currentElement.Add(new XAttribute(attributeName, value));
            }
        }

        // Получение XML-документа
        public string GetXml()
        {
            return _currentElement?.ToString();
        }
    }
}