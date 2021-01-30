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
using System.Windows.Shapes;
using Visualizer.Helpers;
using Visualizer.Visual;

namespace Visualizer.PagesAndWindows.Windows
{
    public partial class EmissionSettingsWindow : Window
    {
        public EmissionSettingsWindow()
        {
            DataContext = FontSizeController.GetInstance();
            InitializeComponent();
            EmissionPowerTB.Text = Metadata.SMetadata.FieldMetadata.EmissionPower.ToString();
            EmissionStepTB.Text = Metadata.SMetadata.FieldMetadata.EmissionStep.ToString();
            HasEmissionCB.IsChecked = Metadata.SMetadata.FieldMetadata.HasEmission;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => ((TextBox)sender).NumberValidation();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(EmissionStepTB.Text, out int emissionStep) &&
                int.TryParse(EmissionPowerTB.Text, out int emissionPower))
            {
                Metadata.SMetadata.FieldMetadata.EmissionPower = emissionPower;
                Metadata.SMetadata.FieldMetadata.EmissionStep = emissionStep;
                Metadata.SMetadata.FieldMetadata.HasEmission = (bool)HasEmissionCB.IsChecked;
                Close();
            }
            else
                CustomMessageBox.Show("Что- то пошло не так");
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click_1(object sender, RoutedEventArgs e) => Close();
    }
}
