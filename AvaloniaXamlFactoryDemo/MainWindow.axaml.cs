using System;
using System.Collections;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Xaml.Factory;
using Avalonia.Xaml.Factory.Generators;
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
                    var view = new Viewbox
                {
                    Stretch = Stretch.Fill,
                    Width = 100,
                    Height = 300,
                    Child = new Ellipse
                    {
                        Width = 50,
                        Height = 50,
                        Fill = Brushes.CornflowerBlue
                    }
                };
                    
                // Создание пользовательского контрола (пример)
                var targetControl = new TestControl();
                
                // Создаем XAML-документ
                var builder = new XamlDocumentBuilder();

                // Генерация элементов и атрибутов с использованием генераторов
                ElementGenerator.GenerateElement(targetControl, builder);

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
