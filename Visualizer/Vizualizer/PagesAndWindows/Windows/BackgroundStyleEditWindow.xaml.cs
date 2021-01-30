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
using Visualizer.Data.Metadata;
using Visualizer.Helpers;
using Visualizer.Visual;

namespace Visualizer.PagesAndWindows
{
    public partial class BackgroundStyleEditWindow : Window
    {
        private List<BackgroundStyle> CloneOfMetadataCustomStyles { get; set; }
        private const string Pattern = "A: (.*?) R: (.*?) G: (.*?) B: (.*?) Offset: (.*?)StopPoint";
        private const string AddNewStyleMessage = "Добаить новый стиль";
        private int LastSelectedIndex { get; set; }
        private bool SelectCompleted { get; set; }
        private bool IsUpdating { get; set; }
        public BackgroundStyleEditWindow()
        {
            InitializeComponent();
            InitializeCloneOfMetadataCustomStyles();
            SetupStylesComboBox();
            SetupSEPointControls();
            UpdateLastSelectedIndex();
            Loaded += (s, e) => RefreshColorBox((ListBoxItem)StyleColorsListBox.SelectedItem);
        }
        private void SetupSEPointControls()
        {
            var currentStlye = CloneOfMetadataCustomStyles[CustomStylesComboBox.SelectedIndex];
            StartPointXControl.Slider.Value = currentStlye.StartPoint.X;
            StartPointYControl.Slider.Value = currentStlye.StartPoint.Y;
            EndPointXControl.Slider.Value = currentStlye.EndPoint.X;
            EndPointYControl.Slider.Value = currentStlye.EndPoint.Y;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void AddNewPoint_Click(object sender, RoutedEventArgs e)
        {
            var colorToAdd = Color.FromRgb(255, 255, 255);
            AddNewItemToStyleColorListBox(colorToAdd, 0);
            CloneOfMetadataCustomStyles[CustomStylesComboBox.SelectedIndex].StylePoints.Add(new StylePoint() { Color = colorToAdd});
            StyleColorsListBox.SelectedIndex = StyleColorsListBox.Items.Count - 1;
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
            CloneOfMetadataCustomStyles[currentCustomStyleIndex].StylePoints.RemoveAt(currentPoint);
            StyleColorsListBox.SelectedIndex = currentPoint - 1;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveChangesToMetadata();
            Close();
        }
        private void SaveChangesToMetadata()
        {
            SaveColorStylesToMetadata();
            Metadata.SMetadata.GraphicsMetadata.SetCurrentBackgroundStyle(CustomStylesComboBox.SelectedIndex);
        }
        private void SaveColorStylesToMetadata()
        {
            SetCurrentStyleFromListBoxItems();
            Metadata.SMetadata.GraphicsMetadata.BackgroundStyles = CloneOfMetadataCustomStyles;
        }
        private void SetCurrentStyleFromListBoxItems() => SetCurrentStyleFromListBoxItems(CustomStylesComboBox.SelectedIndex);
        private void SetCurrentStyleFromListBoxItems(int index)
        {
            if (CloneOfMetadataCustomStyles.Count <= index || !IsLoaded)
                return;
            CloneOfMetadataCustomStyles[index].StylePoints = new List<StylePoint>();
            foreach (var item in StyleColorsListBox.Items)
            {
                var gradientStop = GetGradientStopFromListBoxItem((ListBoxItem)item);
                CloneOfMetadataCustomStyles[index].StylePoints.Add(new StylePoint()
                {
                    Color = gradientStop.Color,
                    Offset = gradientStop.Offset
                });
            }
            CloneOfMetadataCustomStyles[index].StartPoint = new Point(StartPointXControl.Slider.Value, StartPointYControl.Slider.Value);
            CloneOfMetadataCustomStyles[index].EndPoint = new Point(EndPointXControl.Slider.Value, EndPointYControl.Slider.Value);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)=> Close();
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
        private void SetupStylesComboBox()
        {
            var styleNames = CloneOfMetadataCustomStyles?.Select(i => i.Name).ToList();
            CustomStylesComboBox.SetupComboBox(
                styleNames,
                CustomStylesComboBox_SelectionChanged,
                Metadata.SMetadata.GraphicsMetadata.CurrentBackgroundStyleIndex,
                true);
            foreach (var item in CustomStylesComboBox.Items)
                SetMouseUpEventToComboBoxItem((ComboBoxItem)item);
            UpdateStyleColorsListBox();
            CustomStylesComboBox.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>(AddNewStyleMessage, true));
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
                CloneOfMetadataCustomStyles.Add(new BackgroundStyle(name));
                CustomStylesComboBox.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>(AddNewStyleMessage, true));
                IsUpdating = true;
                CustomStylesComboBox.SelectedIndex = CustomStylesComboBox.Items.Count - 2;
                IsUpdating = false;
            }
            else
            {
                if (!IsUpdating)
                    SetCurrentStyleFromListBoxItems(LastSelectedIndex);
                UpdateStyleColorsListBox();
                var currentStyle = CloneOfMetadataCustomStyles[CustomStylesComboBox.SelectedIndex];
                StartPointXControl.Slider.Value = currentStyle.StartPoint.X;
                StartPointYControl.Slider.Value = currentStyle.StartPoint.Y;
                EndPointXControl.Slider.Value = currentStyle.EndPoint.X;
                EndPointYControl.Slider.Value = currentStyle.EndPoint.Y;
                StyleColorsListBox.SelectedIndex = 0;
            }
            UpdateLastSelectedIndex();
        }
        private void UpdateLastSelectedIndex() => LastSelectedIndex = CustomStylesComboBox.SelectedIndex;
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
        private GradientStop GetGradientStopFromListBoxItem(ListBoxItem listBoxItem)
        {
            var currentItemContent = listBoxItem.Content.ToString();
            var content = $"{currentItemContent}StopPoint";
            var match = Regex.Match(content, Pattern);

            var A = byte.Parse(match.Groups[1].Value);
            var R = byte.Parse(match.Groups[2].Value);
            var G = byte.Parse(match.Groups[3].Value);
            var B = byte.Parse(match.Groups[4].Value);
            var offset = double.Parse(match.Groups[5].Value);

            return new GradientStop() { Color = Color.FromArgb(A, R, G, B), Offset = offset };
        }
        private void UpdateStyleColorsListBox()
        {
            StyleColorsListBox.Items.Clear();
            var styleIndex = CustomStylesComboBox.SelectedIndex;
            if (styleIndex == -1)
                return;
            foreach (var point in CloneOfMetadataCustomStyles[styleIndex].StylePoints)
                AddNewItemToStyleColorListBox(point.Color, point.Offset);
            StyleColorsListBox.SelectedIndex = 0; 
        }
        private void AddNewItemToStyleColorListBox(Color color, double offset)
        {
            var listBoxItem = CreateElementHelper.GetBoxItem<ListBoxItem>($"A: {color.A} R: {color.R} G: {color.G} B: {color.B} Offset: {offset}", true);
            listBoxItem.Selected += ListBoxItem_Selected;
            listBoxItem.MouseRightButtonUp += (s, e) =>
            {
                var messageBoxResult = CustomMessageBox.Show("Вы уверены, что хотите удалить данный цвет?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (StyleColorsListBox.Items.Count == 1)
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
                    CloneOfMetadataCustomStyles[CustomStylesComboBox.SelectedIndex].StylePoints.RemoveAt(index);
                    StyleColorsListBox.SelectedIndex = 0;
                }
            };
            StyleColorsListBox.Items.Add(listBoxItem);
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ShowDeletePointButton();
            SetCurrentStyleFromListBoxItems();
            var gradientStop = GetGradientStopFromListBoxItem((ListBoxItem)sender);

            var currentA = gradientStop.Color.A;
            var currentR = gradientStop.Color.R;
            var currentG = gradientStop.Color.G;
            var currentB = gradientStop.Color.B;
            var currentOffset = gradientStop.Offset;

            SelectCompleted = false;
            DownA.Slider.Value = currentA;
            DownR.Slider.Value = currentR;
            DownG.Slider.Value = currentG;
            DownB.Slider.Value = currentB;
            OffsetSlider.Slider.Value = currentOffset;
            SelectCompleted = true;

            RefreshColorBox((ListBoxItem)e.OriginalSource);
        }
        private void RefreshColorBox(ListBoxItem newItem)
        {
            if (!IsLoaded)
                return;
            RefreshAllColorsBrushBorderBackground(); 
            RefreshCurrentColorBrushBorderBackground(newItem);
        }
        private void RefreshAllColorsBrushBorderBackground()
        {
            var allColorsBrush = new LinearGradientBrush();
            allColorsBrush.StartPoint = new Point(StartPointXControl.Slider.Value, StartPointYControl.Slider.Value);
            allColorsBrush.EndPoint = new Point(EndPointXControl.Slider.Value, EndPointYControl.Slider.Value);
            foreach (var item in StyleColorsListBox.Items)
                allColorsBrush.GradientStops.Add(GetGradientStopFromListBoxItem((ListBoxItem)item));
            AllColorsBrushBorder.Background = allColorsBrush;
        }
        private void RefreshCurrentColorBrushBorderBackground(ListBoxItem newItem)
        {
            var currentColorsBrush = new SolidColorBrush();
            currentColorsBrush.Color = GetGradientStopFromListBoxItem(newItem).Color;
            CurrentColorBrushBorder.Background = currentColorsBrush;
        }
        private void InitializeCloneOfMetadataCustomStyles()
        {
            if (Metadata.SMetadata.GraphicsMetadata.BackgroundStyles == null)
                return;
            CloneOfMetadataCustomStyles = new List<BackgroundStyle>(Metadata.SMetadata.GraphicsMetadata.BackgroundStyles.Count);
            foreach (var backgroundStyle in Metadata.SMetadata.GraphicsMetadata.BackgroundStyles)
                CloneOfMetadataCustomStyles.Add(backgroundStyle.Clone());
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
            var currentOffset = OffsetSlider.Slider.Value;
            item.Content = $"A: {currentA} R: {currentR} G: {currentG} B: {currentB} Offset: {currentOffset}";
            RefreshColorBox((ListBoxItem)StyleColorsListBox.SelectedItem);
        }
    }
}
