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

namespace Visualizer.PagesAndWindows
{
    public partial class TextBoxDialogWindow : Window
    {
        private string Result { get; set; }
        private TextBoxDialogWindow()
        {
            DataContext = FontSizeController.GetInstance();
            InitializeComponent();
            BusyNames = new List<string>();
            Loaded += (s, e) => MainTextBox.Focus();
        }
        private List<string> BusyNames { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MainTextBox.Text;
            if(result == "")
            {
                CustomMessageBox.Show("Ошибка! Имя не может быть пустым.");
                return;
            }
            foreach (var item in BusyNames)
                if (result == item)
                {
                    CustomMessageBox.Show("Ошибка! Данное имя занято или является системным. Введите другое.");
                    MainTextBox.Text = "";
                    return;
                }
            Result = result;
            Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        public static string ShowAndGetResult(string message, List<string> busyNames = null)
        {
            var dialogWindow = new TextBoxDialogWindow();

            dialogWindow.MessageTextBlock.Text = message;

            if (busyNames != null)
                foreach (var item in busyNames)
                    dialogWindow.BusyNames.Add(item);

            dialogWindow.ShowDialog();

            return dialogWindow.Result;
        }
    }
}
