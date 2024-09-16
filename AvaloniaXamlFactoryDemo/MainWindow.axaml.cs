using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Factory;
using Avalonia.Xaml.Factory.Generators.Controls;
using Avalonia.Xaml.Factory.Generators.Controls.Grid;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;

namespace AvaloniaXamlFactoryDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Инициализация фабрики и генераторов
            var gridDefinitionGenerator = new GridDefinitionGenerator();
            var controlAttributeGenerator = new ControlAttributeGenerator();
            var elementGeneratorFactory = new ElementGeneratorFactory(gridDefinitionGenerator, controlAttributeGenerator);

            // Инициализация AxamlGenerator
            var axamlBuilder = new AxamlBuilder(elementGeneratorFactory);

            // Создание интерфейса (пример)
            var targetControl = new MyUserControl();

            // Генерация AXAML
            XDocument axamlDoc = axamlBuilder.Build(targetControl);

            // Отображение сгенерированного AXAML в TextEditor
            DisplayGeneratedAxaml(axamlDoc.ToString(), targetControl);
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
