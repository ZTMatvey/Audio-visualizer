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

namespace Visualizer.PagesAndWindows
{
    public partial class PointEditDialogWindow : Window
    {
        private Point Result { get; set; }
        public static Point ShowAndGetResult(Point inputPoint)
        {
            var pointEditDialogWindow = new PointEditDialogWindow(inputPoint);
            pointEditDialogWindow.ShowDialog();
            return pointEditDialogWindow.Result;
        }
        private PointEditDialogWindow(Point inputPoint)
        {
            DataContext = FontSizeController.GetInstance();
            InitializeComponent();
            XTextBox.Text = inputPoint.X.ToString();
            YTextBox.Text = inputPoint.Y.ToString();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => ((TextBox)sender).NumberValidation();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (XTextBox.Text != "" && YTextBox.Text != "")
            {
                Result = new Point(int.Parse(XTextBox.Text), int.Parse(YTextBox.Text));
                Close();
            }
            else
                CustomMessageBox.Show("Ошибка! Поля не могут быть пустыми");
        }
    }
}
