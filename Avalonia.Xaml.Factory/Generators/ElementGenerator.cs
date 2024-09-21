using Avalonia.Controls;

namespace Avalonia.Xaml.Factory.Generators
{
    public static class ElementGenerator
    {
        public static void GenerateElement(Control control, XamlDocumentBuilder builder)
        {
            // Начало элемента
            builder.CreateElement(control.GetType().Name);

            // Генерация свойств контрола
            ElementFactory.GenerateProperties(control, builder);

            // Генерация для дочерних элементов
            GenerateChildren(control, builder);

            // Завершение элемента
            builder.EndElement();
        }

        // Оптимизированная генерация вложенных элементов
        private static void GenerateChildren(Control control, XamlDocumentBuilder builder)
        {
            // Обрабатываем ContentControl (например, Button, Label)
            if (control is ContentControl contentControl && contentControl.Content is Control content)
            {
                // Генерируем дочерний элемент для Content
                GenerateElement(content, builder);
                return;
            }

            // Обрабатываем ItemsControl (например, ListBox, ComboBox, Menu)
            if (control is ItemsControl itemsControl)
            {
                foreach (var item in itemsControl.Items)
                {
                    // Если элемент является контролом, генерируем его как есть
                    if (item is Control itemControl)
                    {
                        GenerateElement(itemControl, builder);
                    }
                    else
                    {
                        // Если элемент скалярный или строка, оборачиваем его в ListBoxItem с TextBlock
                        GenerateScalarOrStringAsListBoxItem(item, builder);
                    }
                }
                return;
            }

            // Обрабатываем контейнеры с Children (например, StackPanel, Grid)
            if (control is Panel panel)
            {
                foreach (var child in panel.Children)
                {
                    if (child is Control childControl)
                    {
                        // Генерация для каждого дочернего элемента
                        GenerateElement(childControl, builder);
                    }
                }
            }

        }

        // Генерация ListBoxItem для скалярных типов и строк
        private static void GenerateScalarOrStringAsListBoxItem(object item, XamlDocumentBuilder builder)
        {
            // Создаем элемент ListBoxItem
            builder.CreateElement("ListBoxItem");

            // Создаем элемент TextBlock внутри ListBoxItem
            builder.CreateElement("TextBlock");
            builder.AddAttribute("Text", item?.ToString() ?? "null");

            // Завершаем элемент TextBlock
            builder.EndElement();

            // Завершаем элемент ListBoxItem
            builder.EndElement();
        }
    }
}
