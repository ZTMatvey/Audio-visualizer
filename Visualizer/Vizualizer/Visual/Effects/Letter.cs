using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Vizualizer.Visual.Effects
{
    public class Letter
    {
        System.Windows.Threading.Dispatcher Dispatcher { get; set; }
        private TextBlock CurrentTextBlock { get; set; }
        private bool IsToBlack { get; set; }
        public Letter(TextBlock TB, System.Windows.Threading.Dispatcher dispatcher, bool isToBlack)
        {
            TB.MouseLeave += (s, e) => { shouldStop = true; SetNormalColor(); };
            TB.MouseEnter += TextBlock_MouseEnter;
            Dispatcher = dispatcher;
            IsToBlack = isToBlack;
            CurrentTextBlock = TB;
            CurrentTextBlock.Foreground = new SolidColorBrush(IsToBlack ? Color.FromArgb(255, 255, 255, 255) : Color.FromArgb(255, 0, 0, 0));
            TB.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
                    IsToBlack = !IsToBlack;
            };
        }
        private const int Step = 4;
        private bool shouldStop { get; set; }
        private Color color;
        private bool ShouldStopSetter { get; set; }
        private async void SetNormalColor()
        {
            ShouldStopSetter = false;
            await Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        if (IsToBlack)
                        {
                            if (color.R <= 250)
                                color.R += Step;
                            else
                                color.R = 255;
                            if (color.G <= 250)
                                color.G += Step;
                            else
                                color.G = 255;
                            if (color.B <= 250)
                                color.B += Step;
                            else
                                color.B = 255;
                        }
                        else
                        {

                            if (color.R >= Step)
                                color.R -= Step;
                            else
                                color.R = 0;
                            if (color.G >= Step)
                                color.G -= Step;
                            else
                                color.G = 0;
                            if (color.B >= Step)
                                color.B -= Step;
                            else
                                color.B = 0;
                        }
                        Dispatcher?.Invoke(new Action(() =>
                        {
                            CurrentTextBlock.Foreground = new SolidColorBrush(color);
                        }));
                        Thread.Sleep(10);
                        if ((color.R == 0 && color.G == 0 && color.B == 0 && IsToBlack) || (color.R == 255 && color.G == 255 && color.B == 255 && !IsToBlack) || ShouldStopSetter)
                            break;
                    }
                }
                catch (Exception) { }
            });
        }

        private async void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            shouldStop = false;
            ShouldStopSetter = true;
            var BlessRNG = new Random();
            byte prevR;
            byte prevG;
            byte prevB;
            if (IsToBlack)
            {
                prevR = 255;
                prevG = 255;
                prevB = 255;
            }
            else
            {
                prevR = 0;
                prevG = 0;
                prevB = 0;
            }
            Action task = () =>
            {
                try
                {
                    while (true)
                    {
                        if (IsToBlack)
                            color = Color.FromArgb(255,
                            BlessRNG.Next(0, 2) == 0 ? (byte)(prevR - Step) : prevR,
                            BlessRNG.Next(0, 2) == 0 ? (byte)(prevG - Step) : prevG,
                            BlessRNG.Next(0, 2) == 0 ? (byte)(prevB - Step) : prevB);
                        else
                            color = Color.FromArgb(255,
                            BlessRNG.Next(0, 2) == 0 ? (byte)(prevR + Step) : prevR,
                            BlessRNG.Next(0, 2) == 0 ? (byte)(prevG + Step) : prevG,
                            BlessRNG.Next(0, 2) == 0 ? (byte)(prevB + Step) : prevB);
                        prevR = color.R;
                        prevG = color.G;
                        prevB = color.B;
                        Dispatcher?.Invoke(new Action(() =>
                        {
                            CurrentTextBlock.Foreground = new SolidColorBrush(color);
                        }));
                        Thread.Sleep(10);
                        if (shouldStop)
                            break;
                    }
                }
                catch (Exception) { }
            };
            await Task.Run(task);
        }

    }
}
