using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Visualizer.Visual;

namespace Visualizer.Helpers
{
    public static class CreateElementHelper
    {
        public static void SetItemFontSizeBinding(this Control control)
        {
            var binding = new Binding()
            {
                Path = new PropertyPath("ItemFontSize"),
                Mode = BindingMode.TwoWay
            };
            control.SetBinding(Control.FontSizeProperty, binding);
        }
        public static T GetBoxItem<T>(string itemContent, bool withFontSizeBinding = false)
            where T : ListBoxItem
        {
            var item = Activator.CreateInstance(typeof(T)) as T;
            item.Content = itemContent;
            if (withFontSizeBinding)
                item.SetItemFontSizeBinding();
            return item;
        }
        public static void SetupComboBox(
            this ComboBox comboBox,
            List<string> items,
            SelectionChangedEventHandler selectionChangedDelegate = null,
            int selectedIndex = -1,
            bool bindingElementFontSize = false)
        {
            if (items != null)
                foreach (var item in items)
                    comboBox.Items.Add(GetBoxItem<ComboBoxItem>(item, bindingElementFontSize));
            if (selectionChangedDelegate != null)
                comboBox.SelectionChanged += selectionChangedDelegate;
            comboBox.SelectedIndex = selectedIndex;
        }
        public static void SetupComboBox(
            this ComboBox comboBox,
            Type enumType,
            SelectionChangedEventHandler selectionChangedDelegate = null,
            int selectedIndex = -1,
            bool bindingElementFontSize = false)
        {
            var enumValues = Enum.GetValues(enumType);
            var enumValuesInStrings = new List<string>(enumValues.Length);
            foreach (var item in enumValues)
                enumValuesInStrings.Add(item.ToString());

            comboBox.SetupComboBox(enumValuesInStrings, selectionChangedDelegate, selectedIndex, bindingElementFontSize);
        }
        public static void SetupFontSizes(this Grid grid)
        {
            foreach (var gridChild in grid.Children)
                if (gridChild is TextBlock textBlock)
                    textBlock.FontSize = Metadata.SMetadata.FontSize;
                else if (gridChild is ComboBox comboBox)
                {
                    comboBox.FontSize = Metadata.SMetadata.ItemsFontSize;
                    foreach (var comboBoxItem in comboBox.Items)
                        ((ComboBoxItem)comboBoxItem).FontSize = Metadata.SMetadata.ItemsFontSize;
                }
                else if (gridChild is Button button)
                    button.FontSize = Metadata.SMetadata.ItemsFontSize;
                else if (gridChild is TextBox textBox)
                    textBox.FontSize = Metadata.SMetadata.ItemsFontSize;
                else if (gridChild is Grid sGrid)
                    sGrid.SetupFontSizes();
        }
    }
}
