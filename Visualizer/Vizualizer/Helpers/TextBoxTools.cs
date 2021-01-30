using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Visualizer.Helpers
{
    public static class TextBoxTools
    {
        private static bool IsChecking { get; set; }
        public static void NumberValidation(this TextBox textBox)
        {
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
        public static void NumberValidationWithDot(this TextBox textBox, int quantitySymbolsBeforeDot = 5)
        {
            if (IsChecking)
                return;
            IsChecking = true;
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                var hasDot = false;
                var currentQuanSymbolsBeforeDot = 0;
                foreach (var item in textBox.Text)
                {
                    if (item == '.')
                    {
                        if (!hasDot)
                            hasDot = true;
                        else
                        {
                            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                            textBox.SelectionStart = textBox.Text.Length;
                            return;
                        }
                    }
                    else if (Regex.IsMatch(item.ToString(), "[^0-9]"))
                    {
                        textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                        textBox.SelectionStart = textBox.Text.Length;
                        return;
                    }
                    if (hasDot)
                    {
                        if (currentQuanSymbolsBeforeDot++ > quantitySymbolsBeforeDot)
                        {
                            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                            textBox.SelectionStart = textBox.Text.Length;
                            return;
                        }
                    }

                }
            }
            IsChecking = false;
        }
        public static void RemoveLastChar(this TextBox textBox)
        {
            if (textBox.Text.Length != 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
    }
}
