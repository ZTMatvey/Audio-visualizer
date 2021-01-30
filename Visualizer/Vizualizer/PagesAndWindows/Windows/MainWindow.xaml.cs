using CSCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using Visualizer.PagesAndWindows;
using Visualizer.PagesAndWindows.Windows;
using Visualizer.Visual;
using VizualizerAC;
using Menu = Visualizer.PagesAndWindows.Menu;

namespace Vizualizer
{
    public partial class MainWindow : System.Windows.Window
    {
        public static Dispatcher MDispatcher { get; set; }
        public static ContentPresenter MContentPresenter { get; set; }
        public static MainWindow MMainWindow { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MDispatcher = Dispatcher;
            MMainWindow = this;
            ApplicationController.Setup();
            ActivatePage(new Visualizer.PagesAndWindows.Menu(), true);
            var a = System.Windows.Application.Current.Resources.MergedDictionaries[0];
            KeyDown += (s, e) =>
            {
                if (ContentPresenter.Content.GetType() != typeof(Visualizer.PagesAndWindows.Menu))
                {
                    if (e.Key == System.Windows.Input.Key.Escape)
                    {
                        if (ContentPresenter.Content is Visualizer.PagesAndWindows.VisualizerPage vb)
                            vb.Controller.StopVizualize();
                        ActivatePage(new Visualizer.PagesAndWindows.Menu());
                    }
                }
            };
            Closing += (s, e) =>
            {
                if (ContentPresenter.Content is VisualizerPage vb)
                {
                    vb.Controller.StopVizualize();
                    ContentPresenter.Content = new Menu();
                }
                ApplicationController.Exit();
            };
        }
        public static void ActivatePage(System.Windows.Controls.UserControl userControl, bool IsShortOpen = false)
        {

            Task.Run(() =>
            {
                if (IsShortOpen)
                    MDispatcher.Invoke(new Action(() => { MMainWindow.Opacity = 0; }));
                else
                    for (int i = 0; i < 10; i++)
                    {
                        MDispatcher.Invoke(new Action(() => { MMainWindow.Opacity -= 0.1; }));
                        Thread.Sleep(10);
                    }
                MDispatcher.Invoke(new Action(() => { MMainWindow.ContentPresenter.Content = userControl; }));
                for (int i = 0; i < 10; i++)
                {
                    MDispatcher.Invoke(new Action(() => { MMainWindow.Opacity += 0.1; }));
                    Thread.Sleep(10);
                }
            });
        }
    }

}
