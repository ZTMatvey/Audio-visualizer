using System;
using System.Windows;
using System.Windows.Controls;
using Visualizer.Helpers;

namespace Visualizer.PagesAndWindows.Controls
{
    public partial class SliderControl : UserControl
    {
        private TextBox TextBox { get; set; }
        private bool isOnlyDoubleNumbers { get; set; }
        public SliderControl()
        {
            DataContext = FontSizeController.GetInstance();
            InitializeComponent();
            NumberTB.LostFocus += (s, e) => { if (((TextBox)s).Text == "") ((TextBox)s).Text = "0"; };
        }
        public static DependencyProperty TextProperty =
               DependencyProperty.Register(
                   nameof(Text),
                   typeof(string),
                   typeof(SliderControl),
                   new PropertyMetadata("", OnTextChanged));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderControl control)
                control.Content.Text = e.NewValue.ToString();
        }

        public static DependencyProperty ValueProperty =
               DependencyProperty.Register(
                   nameof(Value),
                   typeof(int),
                   typeof(SliderControl),
                   new PropertyMetadata(0, OnValueChanged));
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderControl control)
                control.Slider.Value = (int)e.NewValue;
        }
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(TextProperty, value.ToString());
        }
        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }
        public static DependencyProperty MaxValueProperty =
              DependencyProperty.Register(
                  nameof(MaxValue),
                  typeof(int),
                  typeof(SliderControl),
                  new PropertyMetadata(0, OnMaxValueChanged));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderControl control)
                control.Slider.Maximum = (int)e.NewValue;
        }
        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }
        public static DependencyProperty MinValueProperty =
              DependencyProperty.Register(
                  nameof(MinValue),
                  typeof(int),
                  typeof(SliderControl),
                  new PropertyMetadata(0, OnMinValueChanged));

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderControl control)
                control.Slider.Minimum = (int)e.NewValue;
        }


        public static DependencyProperty SliderStyleProperty =
               DependencyProperty.Register(
                   nameof(SliderStyle),
                   typeof(Style),
                   typeof(SliderControl),
                   new PropertyMetadata(null, OnSliderStyleChanged));
        private static void OnSliderStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderControl control)
                control.Slider.Style = (Style)e.NewValue;
        }
        public Style SliderStyle
        {
            get => (Style)GetValue(SliderStyleProperty);
            set => SetValue(SliderStyleProperty, value);
        }

        public bool IsOnlyDoubleNumbers
        {
            get => (bool)GetValue(IsOnlyDoubleNumbersProperty);
            set => SetValue(IsOnlyDoubleNumbersProperty, value);
        }
        public static DependencyProperty IsOnlyDoubleNumbersProperty =
              DependencyProperty.Register(
                  nameof(IsOnlyDoubleNumbers),
                  typeof(bool),
                  typeof(SliderControl),
                  new PropertyMetadata(false, OnTickFrequencyChanged));
        private static void OnTickFrequencyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderControl control)
                control.isOnlyDoubleNumbers = (bool)e.NewValue;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox = ((TextBox)sender);
            if (TextBox.Text == "")
                return;
            if (isOnlyDoubleNumbers)
                TextBox.NumberValidationWithDot(1);
            else
                TextBox.NumberValidation();
            if (double.TryParse(TextBox.Text, out double value))
            {
                if (Slider.Maximum < value)
                {
                    TextBox.RemoveLastChar();
                    return;
                }
                Slider.Value = value;
            }
            else
                TextBox.RemoveLastChar();
        }
    }
}
