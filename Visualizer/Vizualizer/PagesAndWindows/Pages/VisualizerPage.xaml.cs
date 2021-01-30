using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Visualizer.Visual;
using Vizualizer;
using Vizualizer.Visual;

namespace Visualizer.PagesAndWindows
{
    public partial class VisualizerPage : UserControl
    {
        public Controller Controller { get; set; }
        public VisualizerPage()
        {
            InitializeComponent();
            MainWindow.MMainWindow.Cursor = Cursors.None;
            if (Metadata.SMetadata.DeviceIndex == -1)
            {
                CustomMessageBox.Show($"Невозможно запустить визуалайзер так как не выбрано устройство (зайдите в настройки и выберите)");
                MainWindow.ActivatePage(new Menu());
            }
            else
                Loaded += (s, e) =>
                {
                    Controller = new Controller(MGrid, new System.Drawing.Size((int)ActualWidth, (int)ActualHeight), Metadata.SMetadata.GraphicsMetadata.CurrentBackgroundStyle);
                };
        }
    }
}
