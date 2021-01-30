using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Visualizer.PagesAndWindows.Windows { }
namespace Visualizer.PagesAndWindows
{
    public partial class WindSettingsWindow : Window
    {
        public WindSettingsWindow()
        {
            InitializeComponent();

            ChanceForWindTB.Text = Metadata.SMetadata.FieldMetadata.ChanceForWind.ToString();
            MaxWindTimeTB.Text = Metadata.SMetadata.FieldMetadata.MaxWindTime.ToString();
            MinWindTimeTB.Text = Metadata.SMetadata.FieldMetadata.MinWindTime.ToString();
            MaxWindForceTB.Text = Metadata.SMetadata.FieldMetadata.MaxWindForce.ToString();
            MinWindForceTB.Text = Metadata.SMetadata.FieldMetadata.MinWindForce.ToString();
            ShouldShowParticlesCB.IsChecked = Metadata.SMetadata.FieldMetadata.IsWind;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)=> ((TextBox)sender).NumberValidation();

        private void MinWindForceTB_TextChanged(object sender, TextChangedEventArgs e) => ((TextBox)sender).NumberValidationWithDot();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ChanceForWindTB.Text, out int ChanceForWind) &&
                int.TryParse(MaxWindTimeTB.Text, out int MaxWindTime) &&
                int.TryParse(MinWindTimeTB.Text, out int MinWindTime) &&
                double.TryParse(MaxWindForceTB.Text, out double MaxWindForce) &&
                double.TryParse(MinWindForceTB.Text, out double MinWindForce))
            {
                if (MaxWindTime > MinWindTime)
                {
                    if (MaxWindForce > MinWindForce)
                    {
                        if (ChanceForWind != 0 && ChanceForWind <= 100)
                        {
                            Metadata.SMetadata.FieldMetadata.IsWind = (bool)ShouldShowParticlesCB.IsChecked;
                            Metadata.SMetadata.FieldMetadata.ChanceForWind = ChanceForWind;
                            Metadata.SMetadata.FieldMetadata.MaxWindTime = MaxWindTime;
                            Metadata.SMetadata.FieldMetadata.MinWindTime = MinWindTime;
                            Metadata.SMetadata.FieldMetadata.MaxWindForce = MaxWindForce;
                            Metadata.SMetadata.FieldMetadata.MinWindForce = MinWindForce;
                            Close();
                        }
                        else
                            CustomMessageBox.Show("Шанс не может быть равен нулю и не может быть больше ста");
                    }
                    else
                        CustomMessageBox.Show("Максимальная сила должна быть больше минимальной");
                }
                else
                    CustomMessageBox.Show("Максимальное время должно быть больше минимального");
            }
            else
                CustomMessageBox.Show("Что- то пошло не так");
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click_1(object sender, RoutedEventArgs e) => Close();
    }
}
