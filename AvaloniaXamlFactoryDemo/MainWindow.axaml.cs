using System;
using Avalonia.Controls;
using Avalonia.Xaml.Factory;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;

namespace AvaloniaXamlFactoryDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {

                var textBlock = new TextBlock() { Text = "DDDDDDDDDDDDDD" };
                
                // Создание пользовательского контрола (пример)
                var targetControl = new MyUserControl();

                // Создаем XAML-документ
                var builder = new XamlDocumentBuilder();

                // Генерация элементов и атрибутов с использованием генераторов
                ElementGenerator.GenerateElementWithAttributes(targetControl, builder);

                // Получаем сгенерированный XAML
                string axaml = builder.GetXml();

                // Отображение сгенерированного AXAML в TextEditor
                DisplayGeneratedAxaml(axaml, targetControl);
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine($"Ошибка при генерации AXAML: {ex.Message}");
            }
        }

        /// <summary>
        /// Отображение сгенерированного AXAML в TextEditor с синтаксической подсветкой и добавление контрола.
        /// </summary>
        /// <param name="axamlText">Текст AXAML для отображения.</param>
        /// <param name="generatedControl">Сгенерированный контрол для отображения.</param>
        private void DisplayGeneratedAxaml(string axamlText, Control generatedControl)
        {
            if (!string.IsNullOrEmpty(axamlText))
            {
                // Устанавливаем текст AXAML в TextEditor
                AxamlTextEditor.Text = axamlText;

                // Подключаем TextMate для подсветки синтаксиса
                var registryOptions = new RegistryOptions(ThemeName.DarkPlus);
                var textMate = AxamlTextEditor.InstallTextMate(registryOptions);
                textMate.SetGrammar(registryOptions.GetScopeByLanguageId(registryOptions.GetLanguageByExtension(".xml").Id));
            }
            else
            {
                // В случае пустого или null AXAML
                Console.WriteLine("Сгенерированный AXAML пуст или равен null.");
            }

            if (generatedControl != null)
            {
                // Добавляем сгенерированный контрол в ContentControl
                GeneratedControlPlaceholder.Content = generatedControl;
            }
            else
            {
                Console.WriteLine("Сгенерированный контрол равен null.");
            }
        }
    }
}
