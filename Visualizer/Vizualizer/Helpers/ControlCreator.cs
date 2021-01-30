using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Visualizer.Helpers
{
    public static class ControlCreator
    {
        public static Button CreateButton(
            string content,  
            bool withFontSizeBinding,
            RoutedEventHandler clickHandler = null)
        {
            var button = new Button() { Content = content };
            if (withFontSizeBinding)
                button.SetItemFontSizeBinding();
            if (clickHandler != null)
                button.Click += clickHandler;
            return button;
        }
    }
}
