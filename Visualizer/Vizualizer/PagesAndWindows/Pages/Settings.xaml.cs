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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Visualizer.Helpers;
using Visualizer.SoundCaptureAndLineDraw.LineDraw;
using Visualizer.Visual;
using Vizualizer;
using VizualizerAC;

namespace Visualizer.PagesAndWindows
{
    public partial class Settings : UserControl
    {
        public Settings()
        {

            InitializeComponent();
            SetupFPSTextBox();
            SetupCustomSettings();
            SetupScalingStrategy();
            SetupLineCountTextBox();
            SetupDrawLineStgCB();
            SetupShapeTypesCB();

            MGrid.SetupFontSizes();

            OpenPartSysSettingsBut.Click += (s, e) => { new PartcleSystemSettings().Show(); };
        }
        private void SetupDrawLineStgCB()
        {
            DrawLineStgCB.SetupComboBox(typeof(LineSpectrum.DrawLineStrategies), 
                (s, e)=>Metadata.SMetadata.FieldMetadata.DrawLineStrategy = (LineSpectrum.DrawLineStrategies)((ComboBox)s).SelectedIndex, 
                (int)Metadata.SMetadata.FieldMetadata.DrawLineStrategy);
        }
        private void SetupShapeTypesCB()
        {
            ShapeTypesCB.SetupComboBox(typeof(LineSpectrum.ShapeTypes),
                (s, e) => Metadata.SMetadata.FieldMetadata.ShapeType = (LineSpectrum.ShapeTypes)((ComboBox)s).SelectedIndex, 
                (int)Metadata.SMetadata.FieldMetadata.ShapeType);
        }
        private void SetupScalingStrategy()
        {
            ScStrCB.SetupComboBox(typeof(ScalingStrategy), 
                (s, e) =>
            {
                Metadata.SMetadata.FieldMetadata.ScalingStrategy = (ScalingStrategy)((ComboBox)s).SelectedIndex;
            }, (int)Metadata.SMetadata.FieldMetadata.ScalingStrategy);
        }
        private void SetupCustomSettings()
        {
            /*FieldTypeCB.SetupComboBox(
                typeof(FieldType), (s, e) =>
                {
                    Metadata.SMetadata.FieldType = Metadata.SMetadata.FieldType = (FieldType)((ComboBox)s).SelectedIndex;
                    switch (((ComboBox)s).SelectedIndex)
                    {
                        case 0:
                            CustomSettings.IsEnabled = false;
                            CustomSettings.Visibility = Visibility.Hidden;
                            break;
                        case 1:
                            CustomSettings.IsEnabled = true;
                            CustomSettings.Visibility = Visibility.Visible;
                            break;
                    }
                }, (int)Metadata.SMetadata.FieldType);

            switch (Metadata.SMetadata.FieldType)
            {
                case FieldType.Rainbow:
                    CustomSettings.IsEnabled = false;
                    CustomSettings.Visibility = Visibility.Hidden;
                    break;
                case FieldType.Custom:
                    CustomSettings.IsEnabled = true;
                    CustomSettings.Visibility = Visibility.Visible;
                    break;
            }
            FieldTypeCB.FontSize = Metadata.SMetadata.FontSize;*/
            CustomSettings.Click += (s, e) =>
            {
                new CustomSettingsWindow().Show();
            };
            BackgroundSettings.Click += (s, e) =>
              {
                  new BackgroundStyleEditWindow().Show();
              };
        }
        private void SetupFPSTextBox()
        {
            FPSTB.Text = Metadata.SMetadata.FPS.ToString();
            FPSTB.LostFocus += (s, e) =>
            {
                if (int.TryParse(FPSTB.Text, out int res))
                    if (res != 0)
                        Metadata.SMetadata.FPS = res;
                    else
                    {
                        CustomMessageBox.Show("Частота обновления кадров не может быть равна 0");
                        FPSTB.Text = Metadata.SMetadata.FPS.ToString();
                    }
                else
                {
                    CustomMessageBox.Show("Что- то пошло не так");
                    FPSTB.Text = Metadata.SMetadata.FPS.ToString();
                }

            };
        }
        private void SetupLineCountTextBox()
        {
            LineCountTB.Text = Metadata.SMetadata.FieldMetadata.LineCount.ToString();
            LineCountTB.LostFocus += (s, e) =>
            {
                if (int.TryParse(LineCountTB.Text, out int res))
                    if (res != 0)
                        Metadata.SMetadata.FieldMetadata.LineCount = res;
                    else
                    {
                        CustomMessageBox.Show("Количество линий не может быть равно 0");
                        LineCountTB.Text = Metadata.SMetadata.FieldMetadata.LineCount.ToString();
                    }
                else
                {
                    CustomMessageBox.Show("Что- то пошло не так");
                    Metadata.SMetadata.FieldMetadata.LineCount.ToString();
                }

            };
        }
        private void Button_Click(object sender, RoutedEventArgs e) => MainWindow.ActivatePage(new MainSettings());
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => ((TextBox)sender).NumberValidation();
    }
}
