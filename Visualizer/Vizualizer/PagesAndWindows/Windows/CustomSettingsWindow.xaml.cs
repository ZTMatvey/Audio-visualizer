using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Visualizer.Data.Metadata;
using Visualizer.Helpers;
using Visualizer.PagesAndWindows.Windows;
using Visualizer.Visual;

namespace Visualizer.PagesAndWindows
{
    public partial class CustomSettingsWindow : Window
    {
        private List<CustomStyle> CloneOfMetadataCustomStyles { get; set; }
        private const string Pattern = "A: (.*?) R: (.*?) G: (.*?) B: (.*?)StopPoint";
        private const string AddNewStyleMessage = "Добаить новый стиль";
        private int LastSelectedIndex { get; set; }
        private bool SelectCompleted { get; set; }
        private bool IsUpdating { get; set; }
        public CustomSettingsWindow()
        {
            InitializeComponent();
            DataContext = FontSizeController.GetInstance();
            InitializeCloneOfMetadataCustomStyles();
            SetupSmoothingModeCB();
            SetupCompositingQualityCB();
            SetupPixelOffsetModeCB();
            SetupCustomStylesCB();
            HideDeletePointButton();
            SetupPenTextBox();
            SetupPenSizeBindings();
            SelectCompleted = true;
            UpdateLastSelectedIndex();
        }
        private void CustomStylesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomStylesComboBox.Items.Count == 0 || !IsLoaded || CustomStylesComboBox.SelectedIndex == -1)
            {
                UpdateStyleColorsListBox();
                return;
            }
            if (CustomStylesComboBox.SelectedIndex == CustomStylesComboBox.Items.Count - 1)
            {
                var actualSelectedIndex = CustomStylesComboBox.SelectedIndex;
                SetCurrentStyleFromListBoxItems(LastSelectedIndex);
                CustomStylesComboBox.Items.RemoveAt(actualSelectedIndex);
                var busyStyles = CloneOfMetadataCustomStyles.Select(i => i.Name).ToList();
                busyStyles.Add(AddNewStyleMessage);
                var name = TextBoxDialogWindow.ShowAndGetResult(
                    "Введите название стиля:",
                    busyStyles);
                var item = CreateElementHelper.GetBoxItem<ComboBoxItem>(name, true);
                SetMouseUpEventToComboBoxItem(item);
                CustomStylesComboBox.Items.Add(item);
                CloneOfMetadataCustomStyles.Add(new CustomStyle() { Name = name });
                CustomStylesComboBox.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>(AddNewStyleMessage, true));
                IsUpdating = true;
                CustomStylesComboBox.SelectedIndex = CustomStylesComboBox.Items.Count - 2;
                IsUpdating = false;
            }
            else
            {
                if(!IsUpdating)
                    SetCurrentStyleFromListBoxItems(LastSelectedIndex);
                UpdateStyleColorsListBox();
                StyleColorsListBox.SelectedIndex = 0;
            }
            UpdateLastSelectedIndex();
        }
        private void UpdateLastSelectedIndex()=> LastSelectedIndex = CustomStylesComboBox.SelectedIndex;
        private void UpdateStyleColorsListBox()
        {
            StyleColorsListBox.Items.Clear();
            var styleIndex = CustomStylesComboBox.SelectedIndex;
            if (styleIndex == -1)
                return;
            foreach (var color in CloneOfMetadataCustomStyles[styleIndex].Colors)
                AddNewItemToStyleColorListBox(color);
        }
        private void ShowDeletePointButton()
        {
            DeleteCurrentPoint.IsEnabled = true;
            DeleteCurrentPoint.Visibility = Visibility.Visible;
            AddNewPoint.SetValue(Grid.RowSpanProperty, 1);
        }
        private void HideDeletePointButton()
        {
            DeleteCurrentPoint.IsEnabled = false;
            DeleteCurrentPoint.Visibility = Visibility.Hidden;
            AddNewPoint.SetValue(Grid.RowSpanProperty, 2);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveChangesToMetadata();
            Close();
        }
        private void SaveChangesToMetadata()
        {
            Metadata.SMetadata.GraphicsMetadata.SmoothingMode = (SmoothingMode)SmoothingModeCB.SelectedIndex;
            Metadata.SMetadata.GraphicsMetadata.CompositingQuality = (CompositingQuality)CompositionQualityCB.SelectedIndex;
            Metadata.SMetadata.GraphicsMetadata.PixelOffsetMode = (PixelOffsetMode)PixelOffsetModeCB.SelectedIndex;
            Metadata.SMetadata.FieldMetadata.BindingPenSizeToLineSize = (bool)BindingPenSizeToLineSizeCheckBox.IsChecked;
            Metadata.SMetadata.FieldMetadata.PenSize = int.Parse(PenSizeTextBox.Text);
            SaveColorStylesToMetadata();
            Metadata.SMetadata.FieldMetadata.SelectedStyle = CustomStylesComboBox.SelectedIndex;
        }
        private void DeleteCurrentPoint_Click(object sender, RoutedEventArgs e)
        {
            if (StyleColorsListBox.Items.Count == 1)
            {
                CustomMessageBox.Show("Невозможно удалить последнюю точку.");
                return;
            }
            var currentPoint = StyleColorsListBox.SelectedIndex;
            StyleColorsListBox.Items.RemoveAt(currentPoint);
            var currentCustomStyleIndex = CustomStylesComboBox.SelectedIndex;
            CloneOfMetadataCustomStyles[currentCustomStyleIndex].Colors.RemoveAt(currentPoint);
            HideDeletePointButton();
        }
        private System.Drawing.Color GetColorFromListBoxItem(ListBoxItem listBoxItem)
        {
            var currentItemContent = listBoxItem.Content.ToString();
            var content = $"{currentItemContent}StopPoint";
            var match = Regex.Match(content, Pattern);

            var A = byte.Parse(match.Groups[1].Value);
            var R = byte.Parse(match.Groups[2].Value);
            var G = byte.Parse(match.Groups[3].Value);
            var B = byte.Parse(match.Groups[4].Value);

            return System.Drawing.Color.FromArgb(A, R, G, B);
        }
        private void AddNewItemToStyleColorListBox(System.Drawing.Color color)
        {
            var listBoxItem = CreateElementHelper.GetBoxItem<ListBoxItem>($"A: {color.A} R: {color.R} G: {color.G} B: {color.B}", true);
            listBoxItem.Selected += ListBoxItem_Selected;
            listBoxItem.MouseRightButtonUp += (s, e) =>
            {
                var messageBoxResult = CustomMessageBox.Show("Вы уверены, что хотите удалить данный цвет?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if(StyleColorsListBox.Items.Count == 1)
                    {
                        CustomMessageBox.Show("Невозможно удалить последний цвет.");
                        return;
                    }
                    var comboBoxItem = (ListBoxItem)s;
                    var index = 0;
                    for (int i = 0; i < StyleColorsListBox.Items.Count; i++)
                        if (StyleColorsListBox.Items[i] == comboBoxItem)
                        {
                            index = i;
                            break;
                        }
                    if (StyleColorsListBox.SelectedIndex == index)
                        StyleColorsListBox.SelectedIndex = -1;
                    StyleColorsListBox.Items.RemoveAt(index);
                    CloneOfMetadataCustomStyles[CustomStylesComboBox.SelectedIndex].Colors.RemoveAt(index);
                    StyleColorsListBox.SelectedIndex = 0;
                }
            };
            StyleColorsListBox.Items.Add(listBoxItem);
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ShowDeletePointButton();
            SetCurrentStyleFromListBoxItems();
            var color = GetColorFromListBoxItem((ListBoxItem)sender);

            var currentA = color.A;
            var currentR = color.R;
            var currentG = color.G;
            var currentB = color.B;

            SelectCompleted = false;
            DownA.Slider.Value = currentA;
            DownR.Slider.Value = currentR;
            DownG.Slider.Value = currentG;
            DownB.Slider.Value = currentB;
            SelectCompleted = true;

            RefreshColorBox(currentA, currentR, currentG, currentB);
        }
        private void RefreshColorBox(byte A, byte R, byte G, byte B) => ColorBox.Color = System.Windows.Media.Color.FromArgb(A, R, G, B);
        private void AddNewPoint_Click(object sender, RoutedEventArgs e)
        {
            var colorToAdd = System.Drawing.Color.White;
            AddNewItemToStyleColorListBox(colorToAdd);
            CloneOfMetadataCustomStyles[CustomStylesComboBox.SelectedIndex].Colors.Add(colorToAdd);
        }
        private void SaveColorStylesToMetadata()
        {
            SetCurrentStyleFromListBoxItems();
            Metadata.SMetadata.FieldMetadata.CustomStyles = CloneOfMetadataCustomStyles;
        }
        private void SetCurrentStyleFromListBoxItems()=> SetCurrentStyleFromListBoxItems(CustomStylesComboBox.SelectedIndex);
        private void SetCurrentStyleFromListBoxItems(int index)
        {
            if (CloneOfMetadataCustomStyles.Count <= index)
                return;
            CloneOfMetadataCustomStyles[index].Colors = new List<System.Drawing.Color>();
            foreach (var item in StyleColorsListBox.Items)
                CloneOfMetadataCustomStyles[index].Colors.Add(GetColorFromListBoxItem((ListBoxItem)item));
        }
        private void SaveColorStylesToMetadataAndReinitializeCloneOfMetadataCustomStyles()
        {
            SaveColorStylesToMetadata();
            InitializeCloneOfMetadataCustomStyles();
        }
        private void InitializeCloneOfMetadataCustomStyles()
        {
            if (Metadata.SMetadata.FieldMetadata.CustomStyles == null)
                return;
            CloneOfMetadataCustomStyles = new List<CustomStyle>(Metadata.SMetadata.FieldMetadata.CustomStyles.Count);
            foreach (var customStyle in Metadata.SMetadata.FieldMetadata.CustomStyles)
                CloneOfMetadataCustomStyles.Add(customStyle.Clone());
        }
        private void SetupSmoothingModeCB() => SmoothingModeCB.SetupComboBox(
            typeof(SmoothingMode),
            null,
            (int)Metadata.SMetadata.GraphicsMetadata.SmoothingMode,
            true);
        private void SetupCompositingQualityCB() => CompositionQualityCB.SetupComboBox(
            typeof(CompositingQuality),
            null,
            (int)Metadata.SMetadata.GraphicsMetadata.CompositingQuality,
            true);
        private void SetupPixelOffsetModeCB() => PixelOffsetModeCB.SetupComboBox(
            typeof(PixelOffsetMode),
            null,
            (int)Metadata.SMetadata.GraphicsMetadata.PixelOffsetMode,
            true);
        private void SetupCustomStylesCB()
        {
            CustomStylesComboBox.SetupComboBox(
                CloneOfMetadataCustomStyles?.Select(i => i.Name).ToList(),
                CustomStylesComboBox_SelectionChanged,
                Metadata.SMetadata.FieldMetadata.SelectedStyle,
                true);
            foreach (var item in CustomStylesComboBox.Items)
                SetMouseUpEventToComboBoxItem((ComboBoxItem)item);
            UpdateStyleColorsListBox();
            CustomStylesComboBox.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>(AddNewStyleMessage, true));
        }
        private void SetMouseUpEventToComboBoxItem(ComboBoxItem item)
        {
            item.MouseRightButtonUp += (s, e) =>
            {
                var messageBoxResult = CustomMessageBox.Show("Вы уверены, что хотите удалить данный стиль?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var comboBoxItem = (ComboBoxItem)s;
                    var index = 0;
                    for (int i = 0; i < CustomStylesComboBox.Items.Count; i++)
                        if (CustomStylesComboBox.Items[i] == comboBoxItem)
                        {
                            index = i;
                            break;
                        }
                    if (CustomStylesComboBox.SelectedIndex == index)
                        CustomStylesComboBox.SelectedIndex = -1;
                    CustomStylesComboBox.Items.RemoveAt(index);
                    CloneOfMetadataCustomStyles.RemoveAt(index);
                }
            };
        }
        private void DownB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!SelectCompleted)
                return;
            var item = ((ListBoxItem)StyleColorsListBox.SelectedItem);
            if (item == null)
                return;
            var currentA = (byte)DownA.Slider.Value;
            var currentR = (byte)DownR.Slider.Value;
            var currentG = (byte)DownG.Slider.Value;
            var currentB = (byte)DownB.Slider.Value;
            item.Content = $"A: {currentA} R: {currentR} G: {currentG} B: {currentB}";
            RefreshColorBox(currentA, currentR, currentG, currentB);
        }
        private void SetupPenTextBox()
        {
            PenSizeTextBox.Text = Metadata.SMetadata.FieldMetadata.PenSize.ToString();
            PenSizeTextBox.TextChanged += (s, e) => PenSizeTextBox.NumberValidation();
        }
        private void SetupPenSizeBindings()
        {
            BindingPenSizeToLineSizeCheckBox.IsChecked = Metadata.SMetadata.FieldMetadata.BindingPenSizeToLineSize;
            SetPenPartDependences();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void Button_Click_1(object sender, RoutedEventArgs e) => Close();
        private void BindingPenSizeToLineSizeCheckBox_Click(object sender, RoutedEventArgs e) => SetPenPartDependences();
        private void SetPenPartDependences()
        {
            if ((bool)BindingPenSizeToLineSizeCheckBox.IsChecked)
            {
                PenSizeTextBox.IsEnabled = PenSizeMsgTextBlock.IsEnabled = false;
                PenSizeTextBox.Visibility = PenSizeMsgTextBlock.Visibility = Visibility.Hidden;

                BindingPenSizeToLineSizeMsgTextBlock.SetValue(Grid.RowSpanProperty, 2);
                BindingPenSizeToLineSizeCheckBox.SetValue(Grid.RowSpanProperty, 2);
            }
            else
            {
                PenSizeTextBox.IsEnabled = PenSizeMsgTextBlock.IsEnabled = true;
                PenSizeTextBox.Visibility = PenSizeMsgTextBlock.Visibility = Visibility.Visible;

                BindingPenSizeToLineSizeMsgTextBlock.SetValue(Grid.RowSpanProperty, 1);
                BindingPenSizeToLineSizeCheckBox.SetValue(Grid.RowSpanProperty, 1);
            }
        }
    }
}