using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Visualizer.Data.Metadata;
using Visualizer.Helpers;
using Visualizer.Visual;
using VizualizerAC;

namespace Visualizer.PagesAndWindows
{
    public partial class CreateParticleWindow : Window
    {
        private GraphicsPath Path { get; set; }
        private bool NewEvent { get; set; }
        private bool IsEdit { get; set; }
        private List<List<PointF>> FramePoints { get; set; }
        private int CurrentIndex { get; set; }
        private bool EditMode { get; set; }
        public CreateParticleWindow(ParticleType particleType = null)
        {
            DataContext = FontSizeController.GetInstance();
            Path = new GraphicsPath();
            InitializeComponent();
            ImageBox.Source = new Bitmap(500, 500).ImageSourceForBitmap();
            NewEvent = true;
            Flag = true;
            ResolutionSlider.ValueChanged += (s, e) =>
            {
                if (NewEvent && DrawParticle(false) == -1)
                {
                    NewEvent = false;
                    ResolutionSlider.Value = e.OldValue;
                }
                NewEvent = true;
            };
            if (particleType != null)
            {
                FramePoints = particleType.GetCopyFramePointsWithMultiplyOnCompression();
                NameOfParticleTBT.Text = particleType.Name;
                NameOfParticleTBT.IsEnabled = false;
                CurrentIndex = 0;
                Loaded += (s, e) => UpdateGraphicsPathPointAndDrawParticle();
                foreach (var item in FramePoints[CurrentIndex])
                    ListOfParticles.Items.Add(CreateListBoxItem((int)item.X, (int)item.Y));
                if (FramePoints[CurrentIndex].Count > 0)
                {
                    FromXTB.Text = FramePoints[CurrentIndex][FramePoints[CurrentIndex].Count - 1].X.ToString();
                    FromYTB.Text = FramePoints[CurrentIndex][FramePoints[CurrentIndex].Count - 1].Y.ToString();
                    FromXTB.IsEnabled = FromYTB.IsEnabled = false;
                }
                SaveParticleButton.Content = "Сохранить изменения";
                Flag = false;
                IsEdit = true;
            }
            else
            {
                FramePoints = new List<List<PointF>>();
                FramePoints.Add(new List<PointF>());
            }
            SetupCurrentFrameCB();
            Loaded += (s, e) => UpdateFrame();
        }
        private void UpdateFrame()
        {
            UpdateListOfParticles();
            if(FramePoints[CurrentIndex].Count == 0)
            {
                FromYTB.IsEnabled = FromXTB.IsEnabled = true;
                FromYTB.Text = FromXTB.Text = ToXTB.Text = ToYTB.Text = "";
            }
            DrawParticle(false);
        }
        private void UpdateListOfParticles()
        {
            ListOfParticles.Items.Clear();
            foreach (var item in FramePoints[CurrentIndex])
                ListOfParticles.Items.Add(CreateListBoxItem((int)item.X, (int)item.Y));
        }
        private bool Flag { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e) => DrawParticle(true);
        private int DrawParticle(bool isAdd)
        {
            var fromX = 0;
            var toX = 0;
            var fromY = 0;
            var toY = 0;

            if (!isAdd || 
                (int.TryParse(FromXTB.Text, out fromX) && int.TryParse(FromYTB.Text, out fromY)
                && int.TryParse(ToXTB.Text, out toX) && int.TryParse(ToYTB.Text, out toY)))
            {
                if (isAdd)
                {
                    if (Flag) 
                        FramePoints[CurrentIndex].Add(new PointF(fromX, fromY));
                    FramePoints[CurrentIndex].Add(new PointF(toX, toY));
                }
                var tempPath = new GraphicsPath();
                var pointsArray = FramePoints[CurrentIndex].ToArray();
                if (pointsArray.Length > 2)
                {
                    for (int i = 0; i < pointsArray.Length; i++)
                        pointsArray[i] = new PointF((float)(pointsArray[i].X * ResolutionSlider.Value), (float)(pointsArray[i].Y * ResolutionSlider.Value));
                    tempPath.AddPolygon(pointsArray);
                }
                var bitmap = new Bitmap((int)ImageBox.ActualWidth, (int)ImageBox.ActualHeight);
                var graph = Graphics.FromImage(bitmap);
                using (var brush = new SolidBrush(Color.White))
                    graph.FillPath(brush, tempPath);
                ImageBox.Source = bitmap.ImageSourceForBitmap();
                if (isAdd)
                {
                    if (Flag)
                    {
                        ListOfParticles.Items.Add(CreateListBoxItem(fromX, fromY));
                        FromYTB.IsEnabled = FromXTB.IsEnabled = false;
                        Flag = false;
                    }
                    ListOfParticles.Items.Add(CreateListBoxItem(toX, toY));
                    FromYTB.Text = toY.ToString();
                    FromXTB.Text = toX.ToString();
                }
                return 0;
            }
            else
            {
                CustomMessageBox.Show("Что- то пошло не так");
                return -1;
            }
        }
        private ListBoxItem CreateListBoxItem(int x, int y)
        {
            var item = CreateElementHelper.GetBoxItem<ListBoxItem>($"X: {x} Y: {y}", true);
            item.MouseRightButtonUp += (s, e) =>
            {
                var res = CustomMessageBox.Show("Вы уверены, что хотите удалить данную точку?", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    var listBoxItem = (ListBoxItem)s;
                    for (int i = 0; i < ListOfParticles.Items.Count; i++)
                        if (ListOfParticles.Items[i] == listBoxItem)
                        {
                            if (ListOfParticles.Items.Count - 1 == i)
                            {
                                if (ListOfParticles.Items.Count > 1)
                                {
                                    FromYTB.Text = FramePoints[CurrentIndex][i - 1].Y.ToString();
                                    FromXTB.Text = FramePoints[CurrentIndex][i - 1].X.ToString();
                                }
                                else
                                {
                                    FromYTB.Text = "";
                                    FromXTB.Text = "";
                                    FromYTB.IsEnabled = FromXTB.IsEnabled = true;
                                    Flag = true;
                                }
                            }
                            FramePoints[CurrentIndex].RemoveAt(i);
                            ListOfParticles.Items.RemoveAt(i);
                            break;
                        }
                    Path.Reset();
                    for (int i = 0; i < FramePoints[CurrentIndex].Count - 1; i++)
                        Path.AddLine(
                            new PointF(FramePoints[CurrentIndex][i].X, FramePoints[CurrentIndex][i].Y),
                            i + 1 != FramePoints[CurrentIndex].Count ?
                            new PointF(FramePoints[CurrentIndex][i + 1].X, FramePoints[CurrentIndex][i + 1].Y) :
                            new PointF(FramePoints[CurrentIndex][i].X, FramePoints[CurrentIndex][i].Y));
                    DrawParticle(false);
                }
            };
            item.MouseDoubleClick += (sender, arg) =>
            {
                var listBoxItem = ((ListBoxItem)sender);
                var match = Regex.Match($"{listBoxItem.Content.ToString()}stopPoint", "X: (.*?) Y: (.*?)stopPoint");
                var pointX = int.Parse(match.Groups[1].Value.ToString());
                var pointY = int.Parse(match.Groups[2].Value.ToString());
                var newPoint = PointEditDialogWindow.ShowAndGetResult(new System.Windows.Point(pointX, pointY));
                listBoxItem.Content = $"X: {newPoint.X} Y: {newPoint.Y}";
                for (int i = 0; i < ListOfParticles.Items.Count; i++)
                    if (ListOfParticles.Items[i] == listBoxItem)
                    {
                        FramePoints[CurrentIndex][i] = new PointF((float)newPoint.X, (float)newPoint.Y);
                        if(ListOfParticles.Items.Count - 1 == i)
                        {
                            FromXTB.Text = newPoint.X.ToString();
                            FromYTB.Text = newPoint.Y.ToString();
                        }
                        DrawParticle(false);
                        UpdateGraphicsPathPointAndDrawParticle();
                        break;
                    }
            };
            return item;
        }
        private void UpdateGraphicsPathPointAndDrawParticle()
        {
            Path = new GraphicsPath();
            for (int i = 0; i < FramePoints[CurrentIndex].Count - 1; i++)
                Path.AddLine(FramePoints[CurrentIndex][i], FramePoints[CurrentIndex][i + 1]);
            DrawParticle(false);
        }
        private void ToTB_TextChanged(object sender, TextChangedEventArgs e)=> ((TextBox)sender).NumberValidation();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (NameOfParticleTBT.Text != null)
                try
                {
                    var maxX = FramePoints[CurrentIndex].GetMaxX();
                    var maxY = FramePoints[CurrentIndex].GetMaxY();
                    var maxNum = maxX > maxY ? maxX: maxY;

                    Metadata.SMetadata.ParticleTypes.AddUserType(NameOfParticleTBT.Text, FramePoints, (int)maxNum, IsEdit);
                    Close();
                }
                catch (Exception)
                {
                    CustomMessageBox.Show("Частица с таким именем уже существует");
                }
            else
                CustomMessageBox.Show("Введите имя частицы");
        }
        private void SetupCurrentFrameCB()
        {
            for (int i = 0; i < FramePoints.Count; i++)
                CurrentFrameCB.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>((i + 1).ToString(), true));
            CurrentFrameCB.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>("Добавить кадр", true));
            CurrentFrameCB.SelectedIndex = 0;
            CurrentFrameCB.SelectionChanged += CurrentFrameCB_SelectionChanged;
        }
        private void CurrentFrameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EditMode)
                return;
            EditMode = true;
            if(CurrentFrameCB.SelectedIndex == CurrentFrameCB.Items.Count - 1)
            {
                AddNewFrameWithCopyFromPreviousFrame();
                CurrentFrameCB.Items.RemoveAt(CurrentFrameCB.Items.Count - 1);
                CurrentFrameCB.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>((CurrentFrameCB.Items.Count + 1).ToString(), true));
                CurrentFrameCB.Items.Add(CreateElementHelper.GetBoxItem<ComboBoxItem>("Добавить кадр", true));
                Path = new GraphicsPath();
                EditMode = false;
                Flag = true;
                CurrentFrameCB.SelectedIndex = CurrentFrameCB.Items.Count - 2;
            }
            else
            {
                CurrentIndex = CurrentFrameCB.SelectedIndex;
                UpdateFrame();
            }
            EditMode = false;
        }
        private void AddNewFrameWithCopyFromPreviousFrame()
        {
            FramePoints.Add(new List<PointF>(FramePoints[FramePoints.Count - 1].Count));
            for (int i = 0; i < FramePoints[FramePoints.Count - 2].Count; i++)
                FramePoints[FramePoints.Count - 1].Add(FramePoints[FramePoints.Count - 2][i]);
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click_2(object sender, RoutedEventArgs e) => Close();
    }
}
