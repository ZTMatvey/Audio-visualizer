using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Visualizer.Resources;

namespace Visualizer.Visual
{
    public static class ThemesController
    {
        public enum Themes
        {
            Dark,
            Rainbow,
            Autumn
        }
        public static void SetThemeAndUpdateThemeInMetadataAndUpdateStyles(Themes newTheme)
        {
            SetTheme(newTheme);
            UpdateThemeInMetadata(newTheme);
            UpdateAllControlsStyles();
        }
        private static void UpdateAllControlsStyles()
        {
            RemoveAllControlsStyles();
            AddAllControlsStyles();
        }
        private static void AddAllControlsStyles()
        {
            foreach (var item in StylesSources)
            {
                var styleUri = new Uri(item, UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = styleUri });
            }
        }
        private static void RemoveAllControlsStyles()
        {
            if (Application.Current.Resources.MergedDictionaries.Count == Enum.GetValues(typeof(StyleIdexes)).Length)
                for (int i = (int)StyleIdexes.ButtonStyle; i < Application.Current.Resources.MergedDictionaries.Count; i++)
                    Application.Current.Resources.MergedDictionaries.RemoveAt(i);
        }
        private static void UpdateThemeInMetadata(Themes newTheme) => Metadata.SMetadata.Theme = newTheme;
        private static void SetTheme(Themes newTheme)
        {
            Application.Current.Resources.MergedDictionaries[(int)StyleIdexes.ThemePallete] = new ResourceDictionary()
            {
                Source = new Uri(ThemesPathes[(int)newTheme], UriKind.Relative)
            };
        }
        private enum StyleIdexes
        {
            ThemePallete,
            ButtonStyle,
            CheckBoxStyle,
            ComboBoxStyle,
            ListBoxStyle,
            TextBoxStyle,
            TextBlockStyle,
            AlphaSlider,
            RedSlider,
            GreenSlider,
            BlueSlider
        }
        private static string[] ThemesPathes = new string[]
        {
            @"Resources/ColorPalletes/DarkColorPallete.xaml",
            @"Resources/ColorPalletes/RainbowColorPallete.xaml",
            @"Resources/ColorPalletes/AutumnColorPallete.xaml"
        };
        private static readonly string[] StylesSources = new string[]
            {
                "Resources/ButtonStyle.xaml",
                "Resources/CheckBoxStyle.xaml",
                "Resources/ComboBoxStyle.xaml",
                "Resources/ListBoxStyle.xaml",
                "Resources/TextBoxStyle.xaml",
                "Resources/TextBlockStyle.xaml",
                "Resources/Sliders/AlphaSlider.xaml",
                "Resources/Sliders/RedSlider.xaml",
                "Resources/Sliders/GreenSlider.xaml",
                "Resources/Sliders/BlueSlider.xaml"
            };
    }
}
