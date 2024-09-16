using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Xaml.Factory.Generators;
using Avalonia.Xaml.Factory.Generators.Controls;
using Avalonia.Xaml.Factory.Generators.Controls.ContentControl;
using Avalonia.Xaml.Factory.Generators.Controls.Grid;
using Avalonia.Xaml.Factory.Generators.Controls.ListBox;
using Avalonia.Xaml.Factory.Generators.Controls.Menu;
using Avalonia.Xaml.Factory.Generators.Controls.MenuItem;
using Avalonia.Xaml.Factory.Generators.Controls.Panel;
using Avalonia.Xaml.Factory.Generators.Controls.Separator;
using Avalonia.Xaml.Factory.Generators.Controls.TabControl;
using Avalonia.Xaml.Factory.Interfaces;


namespace Avalonia.Xaml.Factory;

/// <summary>
/// Фабрика для выбора подходящего генератора элементов на основе типа контрола.
/// </summary>
public class ElementGeneratorFactory
{
    private readonly Dictionary<Type, IElementGenerator> _generators;
    private readonly GridDefinitionGenerator _gridDefinitionGenerator;
    private readonly ControlAttributeGenerator _controlAttributeGenerator;

    /// <summary>
    /// Конструктор фабрики генераторов элементов.
    /// </summary>
    /// <param name="gridDefinitionGenerator">Генератор сетки для Grid.</param>
    /// <param name="controlAttributeGenerator">Генератор атрибутов для контролов.</param>
    public ElementGeneratorFactory(GridDefinitionGenerator gridDefinitionGenerator,
        ControlAttributeGenerator controlAttributeGenerator)
    {
        _gridDefinitionGenerator = gridDefinitionGenerator;
        _controlAttributeGenerator = controlAttributeGenerator;

        // Создаем словарь с генераторами
        _generators = new Dictionary<Type, IElementGenerator>
        {
            { typeof(Grid), new GridElementGenerator(gridDefinitionGenerator, controlAttributeGenerator, this) },
            { typeof(Panel), new PanelElementGenerator(controlAttributeGenerator, this) },
            { typeof(ContentControl), new ContentControlElementGenerator(controlAttributeGenerator, this) },
            { typeof(TabControl), new TabControlElementGenerator(controlAttributeGenerator, this)} ,
            { typeof(ListBox), new ListBoxElementGenerator(controlAttributeGenerator, this)} ,
            
             /*### Menu ###*/
            { typeof(Menu), new MenuElementGenerator(controlAttributeGenerator, this) }, // Генератор для Menu
           // { typeof(MenuFlyout), new MenuFlyoutElementGenerator(controlAttributeGenerator, this) }, // Генератор для MenuFlyout
            { typeof(MenuItem), new MenuItemElementGenerator(controlAttributeGenerator, this) }, // Генератор для MenuItem
            { typeof(Separator), new SeparatorElementGenerator(controlAttributeGenerator) } // Генератор для Separator
        };
    }

    /// <summary>
    /// Возвращает генератор для заданного контрола.
    /// Если для данного типа контрола нет явного генератора, используется DefaultElementGenerator.
    /// </summary>
    /// <param name="control">Контрол для которого нужен генератор.</param>
    /// <returns>Генератор элемента.</returns>
    public IElementGenerator GetGenerator(Control control)
    {
        var controlType = control.GetType();

        // Возвращаем генератор из словаря, если он есть, иначе используем генератор по умолчанию
        if (_generators.ContainsKey(controlType))
        {
            return _generators[controlType];
        }
        else
        {
            return new DefaultElementGenerator(_controlAttributeGenerator,
                this); // Передача фабрики для работы с дочерними элементами
        }
    }
}