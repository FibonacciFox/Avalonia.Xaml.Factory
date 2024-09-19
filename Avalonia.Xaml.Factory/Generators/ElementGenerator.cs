using Avalonia.Controls;

namespace Avalonia.Xaml.Factory.Generators
{
    public static class ElementGenerator
    {
        public static void GenerateElementWithAttributes(Control control, XamlDocumentBuilder builder)
        {
            // Начало элемента
            builder.CreateElement(control.GetType().Name);

            // Генерация свойств контрола
            ElementFactory.GenerateProperties(control, builder);

            // Рекурсивная генерация для дочерних элементов
            GenerateChildren(control, builder);

            // Завершение элемента
            builder.EndElement();
        }

        // Рекурсивная генерация вложенных элементов
        private static void GenerateChildren(Control control, XamlDocumentBuilder builder)
        {
            // Если элемент имеет свойство Content (например, ContentControl)
            if (control is ContentControl contentControl && contentControl.Content is Control content)
            {
                // Рекурсивно генерируем дочерний элемент для Content
                GenerateElementWithAttributes(content, builder);
            }

            // Если элемент является контейнером с Children (например, StackPanel)
            if (control is Panel panel)
            {
                foreach (var child in panel.Children)
                {
                    if (child is Control childControl)
                    {
                        // Рекурсивно генерируем дочерние элементы для каждого ребенка
                        GenerateElementWithAttributes(childControl, builder);
                    }
                }
            }
        }
    }
}