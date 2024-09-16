using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.ListBox;

    /// <summary>
    /// Генератор для ListBox-контролов.
    /// </summary>
    public class ListBoxElementGenerator : IElementGenerator
    {
        private readonly IAttributeGenerator _attributeGenerator;
        private readonly ElementGeneratorFactory _elementGeneratorFactory;

        /// <summary>
        /// Конструктор генератора для ListBox.
        /// </summary>
        public ListBoxElementGenerator(IAttributeGenerator attributeGenerator, ElementGeneratorFactory elementGeneratorFactory)
        {
            _attributeGenerator = attributeGenerator;
            _elementGeneratorFactory = elementGeneratorFactory;
        }

        /// <summary>
        /// Генерация XML-элемента для ListBox-контролов.
        /// </summary>
        public XElement Generate(Control control)
        {
            var listBox = control as Avalonia.Controls.ListBox;
            var element = new XElement("ListBox");

            // Генерация атрибутов для ListBox с использованием ControlAttributeGenerator
            _attributeGenerator.AddAttributes(listBox, element);

            // Если используется ItemsSource, генерируем привязку данных
            if (listBox.ItemsSource != null)
            {
                element.Add(new XAttribute("ItemsSource", "{Binding}"));  // Пример для привязки данных
            }
            else
            {
                // Генерация статических элементов, оборачивая их в ListBoxItem
                foreach (var item in listBox.Items.OfType<object>())
                {
                    var itemElement = GenerateListBoxItem(item);
                    element.Add(itemElement);
                }
            }

            return element;
        }

        /// <summary>
        /// Генерация ListBoxItem для элементов.
        /// </summary>
        private XElement GenerateListBoxItem(object item)
        {
            var listBoxItemElement = new XElement("ListBoxItem");

            if (item is ListBoxItem listBoxItem)
            {
                // Генерация атрибутов для ListBoxItem с использованием ControlAttributeGenerator
                _attributeGenerator.AddAttributes(listBoxItem, listBoxItemElement);

                // Проверка наличия атрибута Content и добавление текстового содержимого, если атрибута нет
                if (listBoxItem.Content != null && !listBoxItemElement.Attributes("Content").Any())
                {
                    listBoxItemElement.Add(new XText(listBoxItem.Content.ToString()));
                }
            }
            else if (item is Control controlItem)
            {
                // Если элемент - это контрол, оборачиваем его в ListBoxItem
                var controlGenerator = _elementGeneratorFactory.GetGenerator(controlItem);
                var controlElement = controlGenerator.Generate(controlItem);
                listBoxItemElement.Add(controlElement);
            }
            else
            {
                // Если это текст или простой объект, добавляем его напрямую как текст
                listBoxItemElement.Add(new XText(item.ToString()));
            }

            return listBoxItemElement;
        }
    }
