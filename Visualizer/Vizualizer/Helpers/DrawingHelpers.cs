using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualizer.Helpers
{
    public static class DrawingHelpers
    {
        public static float GetMaxX(this PointF[] points)
        {
            var maxX = 0f;
            foreach (var item in points)
                if (item.X > maxX)
                    maxX = item.X;
            return maxX;
        }
        public static float GetMaxY(this PointF[] points)
        {
            var maxY = 0f;
            foreach (var item in points)
                if (item.Y > maxY)
                    maxY = item.Y;
            return maxY;
        }
        public static float GetMaxX(this List<PointF> points)
        {
            var maxX = 0f;
            foreach (var item in points)
                if (item.X > maxX)
                    maxX = item.X;
            return maxX;
        }
        public static float GetMaxY(this List<PointF> points)
        {
            var maxY = 0f;
            foreach (var item in points)
                if (item.Y > maxY)
                    maxY = item.Y;
            return maxY;
        }
        public static float GetMaxX(this List<List<PointF>> points)
        {
            var res = points[0][0].X;
            for (int i = 0; i < points.Count; i++)
                for (int j = 0; j < points[i].Count; j++)
                    if (res > points[i][j].X)
                        res = points[i][j].X;
            return res;
        }
        public static float GetMaxY(this List<List<PointF>> points)
        {
            var res = points[0][0].Y;
            for (int i = 0; i < points.Count; i++)
                for (int j = 0; j < points[i].Count; j++)
                    if(res > points[i][j].Y)
                        res = points[i][j].Y;
            return res;
        }
        public static Bitmap GetScaledImage(this Bitmap source, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / source.Width;
            var ratioY = (double)maxHeight / source.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(source.Width * ratio);
            var newHeight = (int)(source.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(source, 0, 0, newWidth, newHeight);

            return newImage;
        }
        public static void UpdateColorInAllPixels(this Bitmap bitmap, Color newColor)
        {
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    bitmap.SetPixel(x, y, newColor);
        }
        public static void UpdateColorInAllPixels(this Bitmap bitmap, SolidBrush newBrush)
        {
            var newColor = Color.FromArgb(newBrush.Color.A, newBrush.Color.R, newBrush.Color.G, newBrush.Color.B);
            bitmap.UpdateColorInAllPixels(newColor);
        }
    }
}
