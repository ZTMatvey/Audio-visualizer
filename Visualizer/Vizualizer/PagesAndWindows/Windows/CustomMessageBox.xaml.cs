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
    public partial class CustomMessageBox : Window
    {
        private CustomMessageBox(string messsage, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            DataContext = FontSizeController.GetInstance();
            InitializeComponent();
            MessageTB.Text = messsage;
            Button button;
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    button = ControlCreator.CreateButton(
                        "Ок",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.OK; });
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);
                    break;
                case MessageBoxButton.OKCancel:
                    MainDialogGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    MainDialogGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    button = ControlCreator.CreateButton(
                        "Ок",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.OK; });
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);

                    button = ControlCreator.CreateButton(
                        "Отмена",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.Cancel; });
                    button.SetValue(Grid.ColumnProperty, 1);
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);
                    break;
                case MessageBoxButton.YesNoCancel:
                    MainDialogGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    MainDialogGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    MainDialogGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    button = ControlCreator.CreateButton(
                        "Да",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.Yes; });
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);

                    button = ControlCreator.CreateButton(
                        "Нет",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.No; });
                    button.SetValue(Grid.ColumnProperty, 1);
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);

                    button = ControlCreator.CreateButton(
                        "Отмена",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.Cancel; });
                    button.SetValue(Grid.ColumnProperty, 2);
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);
                    break;
                case MessageBoxButton.YesNo:
                    MainDialogGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    MainDialogGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    button = ControlCreator.CreateButton(
                        "Да",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.Yes; });
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);

                    button = ControlCreator.CreateButton(
                        "Нет",
                        true,
                        (s, e) => { DialogResult = true; Result = MessageBoxResult.No; });
                    button.SetValue(Grid.ColumnProperty, 1);
                    button.SetValue(Grid.RowProperty, 1);
                    MainDialogGrid.Children.Add(button);
                    break;
            }
        }
        private MessageBoxResult Result { get; set; }
        public static MessageBoxResult Show(string message, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            var dialog = new CustomMessageBox(message, buttons);
            dialog.ShowDialog();
            return dialog.Result;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)=> DragMove();
    }
}
