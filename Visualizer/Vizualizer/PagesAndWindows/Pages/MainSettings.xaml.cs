using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Visualizer.Helpers;
using Visualizer.Visual;
using Vizualizer;
using VizualizerAC;

namespace Visualizer.PagesAndWindows
{
    public partial class MainSettings : UserControl
    {
        public MainSettings()
        {
            DataContext = FontSizeController.GetInstance();
            InitializeComponent();

            SetupDevicesComboBox();
            SetupThemeComboBox();

            var binding = new Binding();
            binding.Path = new PropertyPath("FontSize");
            binding.Mode = BindingMode.TwoWay;
            FontSizeUserControl.Slider.SetBinding(Slider.ValueProperty, binding);
            binding = new Binding();
            binding.Path = new PropertyPath("ItemFontSize");
            binding.Mode = BindingMode.TwoWay;
            ItemFontSizeUserControl.Slider.SetBinding(Slider.ValueProperty, binding);
        }
        private void SetupDevicesComboBox()
        {
            DevicesCB.SetupComboBox(AudioCapturer.GetDevices().ToList(),
                (s, e) => { Metadata.SMetadata.DeviceIndex = ((ComboBox)s).SelectedIndex; }, -1, true);
            if (DevicesCB.Items.Count - 1 >= Metadata.SMetadata.DeviceIndex)
                DevicesCB.SelectedIndex = Metadata.SMetadata.DeviceIndex;
            else if (DevicesCB.SelectedIndex > 0)
                DevicesCB.SelectedIndex = 0;
            else
                Metadata.SMetadata.DeviceIndex = DevicesCB.SelectedIndex = -1;
        }
        private void SetupThemeComboBox()
        {
            ThemeComboBox.SetupComboBox(
                typeof(ThemesController.Themes), 
                (s, e) =>
                {
                    ThemesController.SetThemeAndUpdateThemeInMetadataAndUpdateStyles((ThemesController.Themes)((ComboBox)s).SelectedIndex);
                }, (int)Metadata.SMetadata.Theme, true);
        }
        private void Button_Click(object sender, RoutedEventArgs e) => MainWindow.ActivatePage(new Settings());
    }
}
