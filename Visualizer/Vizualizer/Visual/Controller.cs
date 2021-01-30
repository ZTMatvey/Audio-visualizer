using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Visualizer.Data.Metadata;
using Visualizer.Visual;
using VizualizerAC;

namespace Vizualizer.Visual
{
    public class Controller
    {
        private Grid MainGrid { get; set; }
        public int QuantityOfColumns { get; set; } = 90;
        private CellsField CellsField { get; set; }
        private AudioCapturer AudioCapturer { get; set; }
        public List<ProgressBar> Bars { get; set; }
        private List<System.Drawing.Color> CloneOfCustomColors { get; set; }
        private bool MakingStep { get; set; }
        private int MaxPointsDataValue { get; set; }
        public Controller(Grid mainGrid, System.Drawing.Size screenSize, BackgroundStyle backgroundStyle)
        {
            MainGrid = mainGrid;
            var brush = new LinearGradientBrush();
            brush.GradientStops = backgroundStyle.GetGradientStopCollection();
            mainGrid.Background = brush;

            CloneOfCustomColors = new List<System.Drawing.Color>();
            foreach (var item in Metadata.SMetadata.FieldMetadata.CustomStyles[Metadata.SMetadata.FieldMetadata.SelectedStyle].Colors)
                CloneOfCustomColors.Add(item);

            AudioCapturer = new AudioCapturer()
            {
                DrawLineStrategy = Metadata.SMetadata.FieldMetadata.DrawLineStrategy,
                Mode = (Mode)Enum.Parse(typeof(Mode), Mode.WasapiLoopbackCapture.ToString()),
                DeviceIndex = Metadata.SMetadata.DeviceIndex,
                HighQuality = true,
                Dispatcher = MainWindow.MDispatcher,
                Size = screenSize,
                FieldType = Metadata.SMetadata.FieldType,
                Interval = Metadata.SMetadata.FPS,
                ScalingStrategy = Metadata.SMetadata.FieldMetadata.ScalingStrategy
            };
            QuantityOfColumns = AudioCapturer.BarCount = Metadata.SMetadata.FieldMetadata.LineCount;
            switch (Metadata.SMetadata.FieldType)
            {
                case FieldType.Rainbow:
                case FieldType.Custom:
                    {
                        CellsField = new CellsField(screenSize, true, CloneOfCustomColors, true, 50);
                        var image = new System.Windows.Controls.Image();
                        image.Source = new Bitmap((int)MainGrid.ActualWidth, (int)MainGrid.ActualHeight).ImageSourceForBitmap();
                        MainGrid.Children.Add(image);
                        image = new System.Windows.Controls.Image();
                        image.Source = new Bitmap((int)MainGrid.ActualWidth, (int)MainGrid.ActualHeight).ImageSourceForBitmap();
                        MainGrid.Children.Add(image);
                        AudioCapturer.Colors = CloneOfCustomColors;
                        AudioCapturer.Image = image;
                    }
                    break;
            }
            if (Metadata.SMetadata.FieldMetadata.ShouldShowParticles)
            {
                CellsField.MaxCells = Metadata.SMetadata.FieldMetadata.MaxParticlesCount;
                CellsField.MaxSize = Metadata.SMetadata.FieldMetadata.MaxParticlesSize;
                CellsField.MinSize = Metadata.SMetadata.FieldMetadata.MinParticlesSize;
                CellsField.MinSpeed = Metadata.SMetadata.FieldMetadata.MinParticlesSpeed;
                CellsField.MaxSpeed = Metadata.SMetadata.FieldMetadata.MaxParticlesSpeed;

                CellsField.IsWind = Metadata.SMetadata.FieldMetadata.IsWind;
                CellsField.MaxWindTime = Metadata.SMetadata.FieldMetadata.MaxWindTime;
                CellsField.MinWindTime = Metadata.SMetadata.FieldMetadata.MinWindTime;
                CellsField.MaxWindForce = Metadata.SMetadata.FieldMetadata.MaxWindForce;
                CellsField.MinWindForce = Metadata.SMetadata.FieldMetadata.MinWindForce;
                CellsField.ChanceForWind = Metadata.SMetadata.FieldMetadata.ChanceForWind;
                CellsField.ScreenSize = screenSize;
                CellsField.TempOfIncreaseAndDecrease = 0.1;//todo тут править
                CellsField.SetupParticles();

                var averageSpeed = (CellsField.MinSize * CellsField.SpeedFactor + CellsField.MaxSize * CellsField.SpeedFactor) / 2;
                averageSpeed = averageSpeed != 0 ? averageSpeed : 1;
                var averageSteps = screenSize.Height / averageSpeed;
                averageSteps = averageSteps != 0 ? averageSteps : 1;
                CellsField.AverageParticlesInOneStep = (int)Math.Round((double)CellsField.MaxCells / averageSteps);
                CellsField.AverageParticlesInOneStep = CellsField.AverageParticlesInOneStep != 0 ? CellsField.AverageParticlesInOneStep : 1;
                MaxPointsDataValue = screenSize.Height * 3;
                AudioCapturer.FieldWasUpdated += (spd) =>
                {
                    Task.Run(() =>
                    {
                        if (!MakingStep)
                        {
                            MakingStep = true;
                            CellsField.MakeStep();
                            if (spd != null)
                            {
                                var cellsToAdd = spd[spd.Length / 2].Value;
                                cellsToAdd += spd[spd.Length - 1].Value;
                                cellsToAdd += spd[0].Value;
                                cellsToAdd /= MaxPointsDataValue;
                                cellsToAdd *= 2 * CellsField.AverageParticlesInOneStep;
                                CellsField.AddCells((int)(Math.Round(cellsToAdd)));
                            }
                            /*
                            var res = 0d;
                            if (spd != null)
                            {
                                foreach (var item in spd)
                                    res += item.Value;
                                var cellsToAdd1 = res / MaxPointsDataValue * 2 * CellsField.AverageParticlesInOneStep;
                                var t = (int)(Math.Round(cellsToAdd1));
                            }*/
                            MainWindow.MDispatcher.Invoke(new Action(() =>
                            {
                                ((System.Windows.Controls.Image)MainGrid.Children[0]).Source = CellsField.GetField();
                            }));
                            MakingStep = false;
                        }
                    });
                };
            }
            AudioCapturer.Start();
        }
        public void StopVizualize() => AudioCapturer.Stop();
        private void SetUpProgressBars()
        {
            var itemIndex = 0;
            for (int i = 0; i < 15; i++)
            {
                var pb = new ProgressBar
                {
                    Value = 0,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, (byte)(i * 16), 0)),
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0)),
                };
                pb.SetValue(Grid.ColumnProperty, itemIndex++);
                MainGrid.Children.Add(pb);
                Bars.Add(pb);
            }
            for (int i = 15; i >= 0; i--)
            {
                var pb = new ProgressBar
                {
                    Value = 0,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)(i * 16), 255, 0)),
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0))
                };
                pb.SetValue(Grid.ColumnProperty, itemIndex++);
                MainGrid.Children.Add(pb);
                Bars.Add(pb);
            }
            for (int i = 0; i < 15; i++)
            {
                var pb = new ProgressBar
                {
                    Value = 0,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, (byte)(i * 16))),
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0))
                };
                pb.SetValue(Grid.ColumnProperty, itemIndex++);
                MainGrid.Children.Add(pb);
                Bars.Add(pb);
            }
            for (int i = 15; i >= 0; i--)
            {
                var pb = new ProgressBar
                {
                    Value = 0,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, (byte)(i * 16), 255)),
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0))
                };
                pb.SetValue(Grid.ColumnProperty, itemIndex++);
                MainGrid.Children.Add(pb);
                Bars.Add(pb);
            }
            for (int i = 0; i < 15; i++)
            {
                var pb = new ProgressBar
                {
                    Value = 0,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)(i * 16), 0, 255)),
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0))
                };
                pb.SetValue(Grid.ColumnProperty, itemIndex++);
                MainGrid.Children.Add(pb);
                Bars.Add(pb);
            }
            for (int i = 15; i >= 0; i--)
            {
                var pb = new ProgressBar
                {
                    Value = 0,
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, (byte)(i * 16))),
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0))
                };
                pb.SetValue(Grid.ColumnProperty, itemIndex++);
                MainGrid.Children.Add(pb);
                Bars.Add(pb);
            }
        }
    }
}
