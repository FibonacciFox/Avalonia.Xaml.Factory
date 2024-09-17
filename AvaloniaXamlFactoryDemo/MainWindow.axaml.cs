using System;
using Avalonia.Controls;
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
            
            // Создание интерфейса (пример)
            var targetControl = new MyUserControl();

            // Создаем XAML документ
            var builder = new XamlDocumentBuilder();

            // Генерация элементов и атрибутов с использованием генераторов
            ElementGenerator.GenerateElementWithAttributes(targetControl, builder);

            // Получаем сгенерированный XAML
            string axaml = builder.GetXml();

            // Отображение сгенерированного AXAML в TextEditor
            DisplayGeneratedAxaml(axaml, targetControl);
        }

        /// <summary>
        /// Отображение сгенерированного AXAML в TextEditor с синтаксической подсветкой и добавление контрола.
        /// </summary>
        /// <param name="axamlText">Текст AXAML для отображения.</param>
        /// <param name="generatedControl">Сгенерированный контрол для отображения.</param>
        private void DisplayGeneratedAxaml(string axamlText, Control generatedControl)
        {
            // Устанавливаем текст AXAML в TextEditor
            AxamlTextEditor.Text = axamlText;

            // Подключаем TextMate для подсветки синтаксиса
            var registryOptions = new RegistryOptions(ThemeName.DarkPlus);
            var textMate = AxamlTextEditor.InstallTextMate(registryOptions);
            textMate.SetGrammar(registryOptions.GetScopeByLanguageId(registryOptions.GetLanguageByExtension(".xml").Id));

            // Добавляем сгенерированный контрол в ContentControl
            GeneratedControlPlaceholder.Content = generatedControl;
        }
    }
}