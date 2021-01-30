using System.Windows;
using Vizualizer;
using System.Windows.Controls;

namespace Visualizer.PagesAndWindows
{
    public partial class Menu : UserControl
    {
        public Menu()
        {
            DataContext = FontSizeController.GetInstance();
            InitializeComponent();
            MainWindow.MMainWindow.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)=> MainWindow.ActivatePage(new VisualizerPage());

        private void Button_Click_1(object sender, RoutedEventArgs e)=> MainWindow.ActivatePage(new MainSettings());

        private void Button_Click_2(object sender, RoutedEventArgs e)=> MainWindow.MMainWindow.Close();
    }
}
