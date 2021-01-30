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
using Visualizer.PagesAndWindows.Windows;
using Visualizer.Visual;

namespace Visualizer.PagesAndWindows
{
    public partial class PartcleSystemSettings : Window
    {
        public static PartcleSystemSettings SPartcleSystemSettings { get; set; }
        public PartcleSystemSettings()
        {
            DataContext = FontSizeController.GetInstance();
            SPartcleSystemSettings = this;
            Closing += (s, e) => { SPartcleSystemSettings = null; };
            InitializeComponent();
            SetupTypesCB();
            ShouldShowParticlesCB.IsChecked = Metadata.SMetadata.FieldMetadata.ShouldShowParticles;
            MaxParticlesCountTB.Text = Metadata.SMetadata.FieldMetadata.MaxParticlesCount.ToString();
            MaxParticlesSizeTB.Text = Metadata.SMetadata.FieldMetadata.MaxParticlesSize.ToString();
            MinParticlesSizeTB.Text = Metadata.SMetadata.FieldMetadata.MinParticlesSize.ToString();
            MinParticlesSpeedTB.Text = Metadata.SMetadata.FieldMetadata.MinParticlesSpeed.ToString();
            MaxParticlesSpeedTB.Text = Metadata.SMetadata.FieldMetadata.MaxParticlesSpeed.ToString();
            UpdateParticleEditButtonStateAndChangeTypeOfParticlesCBColomnSpan();
        }
        private void SetupTypesCB()
        {
            TypeOfParticlesCB.SetupComboBox(Metadata.SMetadata.ParticleTypes.TypeNames,
                (s, e) =>
            {
                switch (TypeOfParticlesCB.SelectedIndex)
                {
                    case 0:
                    case 1:
                    case 2:
                        Metadata.SMetadata.FieldMetadata.ParticalView = ParticalView.Default;
                        Metadata.SMetadata.FieldMetadata.DefParticalType = (DefaultParticleTypes)TypeOfParticlesCB.SelectedIndex;
                        break;
                    default:
                        Metadata.SMetadata.FieldMetadata.ParticalView = ParticalView.Custom;
                        var typeIndex = TypeOfParticlesCB.SelectedIndex;
                        Metadata.SMetadata.FieldMetadata.CustomType = Metadata.SMetadata.ParticleTypes.GetPoints(typeIndex);
                        Metadata.SMetadata.FieldMetadata.CurrentTypeIndex = typeIndex;
                        break;
                }
                UpdateParticleEditButtonStateAndChangeTypeOfParticlesCBColomnSpan();
            },
                Metadata.SMetadata.FieldMetadata.ParticalView == ParticalView.Default ?
                (int)Metadata.SMetadata.FieldMetadata.DefParticalType :
                Metadata.SMetadata.FieldMetadata.CurrentTypeIndex);
            for (int i = Enum.GetValues(typeof(DefaultParticleTypes)).Length; i < TypeOfParticlesCB.Items.Count; i++)
            {
                var item = ((ComboBoxItem)TypeOfParticlesCB.Items[i]);
                item.MouseRightButtonUp += TypeOfParticlesCBItem_MouseRightButtonUp;
            }
        }
        private void UpdateParticleEditButtonStateAndChangeTypeOfParticlesCBColomnSpan()
        {
            if (TypeOfParticlesCB.SelectedIndex < Enum.GetValues(typeof(DefaultParticleTypes)).Length)
                HideParticleEditButtonAndChangeTypeOfParticlesCBColomnSpan();
            else
                ShowParticleEditButtonAndChangeTypeOfParticlesCBColomnSpan();
        }
        private void ShowParticleEditButtonAndChangeTypeOfParticlesCBColomnSpan()
        {
            ParticleEditButton.IsEnabled = true;
            ParticleEditButton.Visibility = Visibility.Visible;
            TypeOfParticlesCB.SetValue(Grid.ColumnSpanProperty, 1);
        }
        private void HideParticleEditButtonAndChangeTypeOfParticlesCBColomnSpan()
        {
            ParticleEditButton.IsEnabled = false;
            ParticleEditButton.Visibility = Visibility.Hidden;
            TypeOfParticlesCB.SetValue(Grid.ColumnSpanProperty, 2);
        }
        private void TypeOfParticlesCBItem_DoubleClcik(object sender, EventArgs e)
        {
            var currentType = Metadata.SMetadata.ParticleTypes.UsersTypes[TypeOfParticlesCB.SelectedIndex - Enum.GetValues(typeof(DefaultParticleTypes)).Length - 1];
            var createParticleWindow = new CreateParticleWindow(currentType);
            createParticleWindow.Show();
        }
        private void TypeOfParticlesCBItem_MouseRightButtonUp(object sender, EventArgs e)
        {
            var messageBoxResult = CustomMessageBox.Show("Вы уверены, что хотите удалить данную частицу?", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var comboBoxItem = (ComboBoxItem)sender;
                for (int j = Enum.GetValues(typeof(DefaultParticleTypes)).Length; j < TypeOfParticlesCB.Items.Count; j++)
                    if (TypeOfParticlesCB.Items[j] == comboBoxItem)
                    {
                        TypeOfParticlesCB.SelectedIndex = 0;
                        TypeOfParticlesCB.Items.RemoveAt(j);
                        break;
                    }
                Metadata.SMetadata.ParticleTypes.RemoveTypeByName(comboBoxItem.Content.ToString());
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)=> ((TextBox)sender).NumberValidation();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MaxParticlesCountTB.Text, out int MaxPC) &&
                int.TryParse(MaxParticlesSizeTB.Text, out int MaxPSize) &&
                int.TryParse(MinParticlesSizeTB.Text, out int MinPSize) &&
                int.TryParse(MaxParticlesSpeedTB.Text, out int MaxPSpd) &&
                int.TryParse(MinParticlesSpeedTB.Text, out int MinPSpd))
            {
                if (MaxPSize > MinPSize)
                {
                    if (MaxPSpd > MinPSpd)
                    {
                        if (MinPSpd != 0)
                        {
                            Metadata.SMetadata.FieldMetadata.ShouldShowParticles = (bool)ShouldShowParticlesCB.IsChecked;
                            Metadata.SMetadata.FieldMetadata.MaxParticlesCount = MaxPC;
                            Metadata.SMetadata.FieldMetadata.MaxParticlesSize = MaxPSize;
                            Metadata.SMetadata.FieldMetadata.MinParticlesSize = MinPSize;
                            Metadata.SMetadata.FieldMetadata.MaxParticlesSpeed = MaxPSpd;
                            Metadata.SMetadata.FieldMetadata.MinParticlesSpeed = MinPSpd;
                            Close();
                        }
                        else
                            CustomMessageBox.Show("Минимальная скорость не может быть равна нулю");
                    }
                    else
                        CustomMessageBox.Show("Максимальная скорость должна быть больше минимальной");
                }
                else
                    CustomMessageBox.Show("Максимальный размер должен быть больше минимального");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)=> new CreateParticleWindow().Show();

        private void Button_Click_2(object sender, RoutedEventArgs e) => new WindSettingsWindow().Show();

        private void Button_Click_3(object sender, RoutedEventArgs e) => new EmissionSettingsWindow().Show();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click_4(object sender, RoutedEventArgs e) => Close();

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var currentType = Metadata.SMetadata.ParticleTypes.UsersTypes[TypeOfParticlesCB.SelectedIndex - Enum.GetValues(typeof(DefaultParticleTypes)).Length];
            var createParticleWindow = new CreateParticleWindow(currentType);
            createParticleWindow.Show();
        }
    }
}
