using System.Linq;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Interfaces;

namespace Avalonia.Xaml.Factory.Generators.Controls.Panel;

    /// <summary>
    /// Генератор элементов для Panel-контролов.
    /// </summary>
    public class PanelElementGenerator : IElementGenerator
    {
        private readonly IAttributeGenerator _attributeGenerator;
        private readonly ElementGeneratorFactory _elementGeneratorFactory;

        /// <summary>
        /// Конструктор генератора для Panel.
        /// </summary>
        /// <param name="attributeGenerator">Генератор атрибутов для контролов.</param>
        /// <param name="elementGeneratorFactory">Фабрика генераторов для дочерних элементов.</param>
        public PanelElementGenerator(IAttributeGenerator attributeGenerator, ElementGeneratorFactory elementGeneratorFactory)
        {
            _attributeGenerator = attributeGenerator;
            _elementGeneratorFactory = elementGeneratorFactory;
        }

        /// <summary>
        /// Генерация XML-элемента для Panel-контролов.
        /// </summary>
        public XElement Generate(Control control)
        {
            var panel = control as Avalonia.Controls.Panel;
            var element = new XElement("Panel");

            // Генерация атрибутов для Panel
            _attributeGenerator.AddAttributes(panel, element);

            // Генерация дочерних элементов
            foreach (var child in panel.Children.OfType<Control>())
            {
                // Используем фабрику для получения генератора для дочерних элементов
                var childElementGenerator = _elementGeneratorFactory.GetGenerator(child);
                var childElement = childElementGenerator.Generate(child);
                element.Add(childElement);
            }

            return element;
        }
    }
