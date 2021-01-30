using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Visualizer.Data.Metadata
{
    public class BackgroundStyle
    {
        public string Name { get; set; }
        public List<StylePoint> StylePoints { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public BackgroundStyle()
        {}
        public BackgroundStyle(string name, bool addDefaultColor = true)
        {
            Name = name;
            var gr = new GradientStop();
            StylePoints = new List<StylePoint>();
            if (addDefaultColor)
                StylePoints.Add(new StylePoint() { Color = Color.FromRgb(0, 0, 0) });
        }
        public BackgroundStyle(BackgroundStyle other)
        {
            Name = other.Name;
            StylePoints = new List<StylePoint>();
            StartPoint = other.StartPoint;
            EndPoint = other.EndPoint;
            ReplaceStyleOrAddStops(other.StylePoints, false);
        }
        public GradientStopCollection GetGradientStopCollection()
        {
            var result = new GradientStopCollection();
            foreach (var item in StylePoints)
                result.Add(new GradientStop() { Color = item.Color, Offset = item.Offset});
            return result;
        }
        public void AddColor(Color colorToAdd, double offset) => StylePoints.Add(new StylePoint() { Color = colorToAdd, Offset = offset });
        public BackgroundStyle Clone() => new BackgroundStyle(this);
        public LinearGradientBrush GetLinearGradientBrush()
        {
            var brush = new LinearGradientBrush();
            brush.GradientStops = GetGradientStopCollection();
            brush.StartPoint = StartPoint;
            brush.EndPoint = EndPoint;
            return brush;
        }
        public void ReplaceStyleOrAddStops(List<StylePoint> newStyle, bool withClear)
        {
            if (withClear)
                StylePoints.Clear();
            foreach (var item in newStyle)
                StylePoints.Add(item.Clone());
        }
    }
}
