using System.Drawing;
using System.Drawing.Drawing2D;
using static VizualizerAC.SpectrumBase;
using static Visualizer.SoundCaptureAndLineDraw.LineDraw.PointPositionCalculator;

namespace Visualizer.SoundCaptureAndLineDraw.LineDraw
{
    public static class LineDrawer
    {
        public static void DrawUpLines(
            Graphics graphics,
            Pen pen,
            SpectrumPointData[] spectrumPoints,
            Size size,
            double barWidth,
            bool withBindingLineSizeToBarSize = false)
        {
            if (withBindingLineSizeToBarSize)
                DrawUpLinesWithBinding(graphics, pen, size, spectrumPoints, barWidth);
            else
                DrawUpLinesWithoutBinding(graphics, pen, spectrumPoints, barWidth);
        }
        public static void DrawBottomLines(
            Graphics graphics,
            Pen pen,
            SpectrumPointData[] spectrumPoints,
            Size size,
            double barWidth,
            bool withBindingLineSizeToBarSize = false)
        {
            if (withBindingLineSizeToBarSize)
                DrawBottomLinesWithBinding(graphics, pen, size, spectrumPoints, barWidth);
            else
                DrawBottomLinesWithoutBinding(graphics, pen, size, spectrumPoints, barWidth);
        }
        public static void DrawBottomAndUp(
            Graphics graphics,
            Pen pen,
            SpectrumPointData[] spectrumPoints,
            Size size,
            double barWidth,
            bool withBindingLineSizeToBarSize = false)
        {
            if (withBindingLineSizeToBarSize)
                DrawBottomAndUpLinesWithBinding(graphics, pen, size, spectrumPoints, barWidth);
            else
                DrawBottomAndUpLinesWithoutBinding(graphics, pen, size, spectrumPoints, barWidth);
        }
        public static void DrawCenter(
            Graphics graphics,
            Pen pen,
            SpectrumPointData[] spectrumPoints,
            Size size,
            double barWidth,
            bool withBindingLineSizeToBarSize = false)
        {
            if (withBindingLineSizeToBarSize)
                DrawCetnterLinesWithBinding(graphics, pen, size, spectrumPoints, barWidth);
            else
                DrawCetnterLinesWithoutBinding(graphics, pen, size, spectrumPoints, barWidth);
        }
        private static void DrawUpLinesWithoutBinding(
            Graphics graphics,
            Pen pen,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            for (int i = 0; i < spectrumPoints.Length; i++)
            {
                path.AddLine(CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i].Value, i, barWidth),
                    i != (spectrumPoints.Length - 1) ?
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + 1].Value, i + 1, barWidth) :
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i].Value, i + 1, barWidth));
            }
            graphics.DrawPath(pen, path);
        }
        private static void DrawUpLinesWithBinding(
            Graphics graphics,
            Pen pen,
            Size size,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            path.AddLine(new PointF(0, 0), new PointF(0, (float)spectrumPoints[0].Value));
            for (int i = 0; i < spectrumPoints.Length; i++)
            {
                path.AddLine(CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i].Value, i, barWidth),
                    i != (spectrumPoints.Length - 1) ?
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + 1].Value, i + 1, barWidth) :
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i].Value, i + 1, barWidth));
            }
            path.AddLine(new PointF(size.Width, (float)spectrumPoints[spectrumPoints.Length - 1].Value), new PointF(size.Width, 0));
            graphics.FillPath(pen.Brush, path);
        }

        private static void DrawBottomLinesWithoutBinding(
            Graphics graphics,
            Pen pen,
            Size size,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            for (int i = 0; i < spectrumPoints.Length; i++)
            {
                path.AddLine(CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i].Value, i, barWidth),
                    i != (spectrumPoints.Length - 1) ?
                    CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i + 1].Value, i + 1, barWidth) :
                    CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i].Value, i + 1, barWidth));
            }
            graphics.DrawPath(pen, path);
        }
        private static void DrawBottomLinesWithBinding(
            Graphics graphics,
            Pen pen,
            Size size,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            path.AddLine(new PointF(0, size.Height), new PointF(0, (float)spectrumPoints[0].Value));
            for (int i = 0; i < spectrumPoints.Length; i++)
            {
                path.AddLine(new PointF((float)(barWidth * i), (float)(size.Height - spectrumPoints[i].Value)),
                    i != (spectrumPoints.Length - 1) ?
                    new PointF((float)(barWidth * (i + 1)), (float)(size.Height - spectrumPoints[i + 1].Value)) :
                    new PointF((float)(barWidth * (i + 1)), (float)(size.Height - spectrumPoints[i].Value)));
            }
            path.AddLine(new PointF(size.Width, (float)spectrumPoints[spectrumPoints.Length - 1].Value), new PointF(size.Width, size.Height));
            graphics.FillPath(pen.Brush, path);
        }

        private static void DrawBottomAndUpLinesWithoutBinding(
            Graphics graphics,
            Pen pen,
            Size size,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            for (int i = 0; i < spectrumPoints.Length / 2; i++)
            {
                path.AddLine(CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i].Value, i, barWidth * 2),
                    i != (spectrumPoints.Length - 1) ?
                    CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i + 1].Value, i + 1, barWidth * 2) :
                    CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i].Value, i + 1, barWidth * 2));
            }
            graphics.DrawPath(pen, path);
            path = new GraphicsPath();
            for (int i = 0; i < spectrumPoints.Length / 2; i++)
            {
                path.AddLine(CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + (spectrumPoints.Length / 2 - 1)].Value, i, barWidth * 2),
                    i != (spectrumPoints.Length - 1) ?
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + 1 + (spectrumPoints.Length / 2 - 1)].Value, i + 1, barWidth * 2) :
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + (spectrumPoints.Length / 2 - 1)].Value, i + 1, barWidth * 2));
            }
            graphics.DrawPath(pen, path);
        }
        private static void DrawBottomAndUpLinesWithBinding(
            Graphics graphics,
            Pen pen,
            Size size,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            path.AddLine(new PointF(0, size.Height), new PointF(0, (float)(size.Height - spectrumPoints[0].Value / 2)));
            for (int i = 0; i < spectrumPoints.Length / 2; i++)
            {
                path.AddLine(CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i].Value / 2, i, barWidth * 2),
                    i != (spectrumPoints.Length - 1) ?
                    CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i + 1].Value / 2, i + 1, barWidth * 2) :
                    CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(size, spectrumPoints[i].Value / 2, i + 1, barWidth * 2));
            }
            path.AddLine(new PointF(size.Width, (float)(size.Height - spectrumPoints[spectrumPoints.Length - 1 - spectrumPoints.Length / 2].Value / 2)), new PointF(size.Width, size.Height));
            graphics.FillPath(pen.Brush, path);

            path = new GraphicsPath();
            path.AddLine(new PointF(0, 0), new PointF(0, (float)spectrumPoints[spectrumPoints.Length / 2 - 1].Value / 2));
            for (int i = 0; i < spectrumPoints.Length / 2; i++)
            {
                path.AddLine(CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + (spectrumPoints.Length / 2 - 1)].Value / 2, i, barWidth * 2),
                    i != (spectrumPoints.Length - 1) ?
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + 1 + (spectrumPoints.Length / 2 - 1)].Value / 2, i + 1, barWidth * 2) :
                    CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(spectrumPoints[i + (spectrumPoints.Length / 2 - 1)].Value / 2, i + 1, barWidth * 2));
            }
            path.AddLine(new PointF(size.Width, (float)(size.Height - spectrumPoints[spectrumPoints.Length - 1].Value / 2)), new PointF(size.Width, 0));
            graphics.FillPath(pen.Brush, path);
        }

        private static void DrawCetnterLinesWithoutBinding(
            Graphics graphics,
            Pen pen,
            Size size,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            for (int i = 0; i < spectrumPoints.Length; i++)
            {
                path.AddLine(CalculateYZeroPositionForCenterStateWithoutBindingLineSizeToBarSize(size, spectrumPoints[i].Value, i, barWidth, i == 0, i % 2 == 0),
                    i != (spectrumPoints.Length - 1) ?
                    CalculateYZeroPositionForCenterStateWithoutBindingLineSizeToBarSize(size, spectrumPoints[i + 1].Value, i + 1, barWidth, false, (i + 1) % 2 == 0) :
                    CalculateYZeroPositionForCenterStateWithoutBindingLineSizeToBarSize(size, spectrumPoints[i].Value, i + 1, barWidth, true, (i + 1) % 2 == 0));
            }
            graphics.DrawPath(pen, path);
        }
        private static void DrawCetnterLinesWithBinding(
            Graphics graphics,
            Pen pen,
            Size size,
            SpectrumPointData[] spectrumPoints,
            double barWidth)
        {
            var path = new GraphicsPath();
            int cuurentPoligonPointsIndex = 0;
            var poligonPoints = new PointF[spectrumPoints.Length + 1];
            poligonPoints[cuurentPoligonPointsIndex++] = new PointF(0, (float)(size.Height/2 - spectrumPoints[0].Value/2));
            for (int i = 1; i < spectrumPoints.Length - 1; i += 2)
                poligonPoints[cuurentPoligonPointsIndex++] = CalculateYZeroPositionForCenterStateWithoutBindingLineSizeToBarSize(
                            size,
                            spectrumPoints[i].Value,
                            i,
                            barWidth,
                            false,
                            false);
            poligonPoints[cuurentPoligonPointsIndex++] = new PointF(size.Width, size.Height/2);
            for (int i = spectrumPoints.Length - 2; i >= 2; i -= 2)
                poligonPoints[cuurentPoligonPointsIndex++] = CalculateYZeroPositionForCenterStateWithoutBindingLineSizeToBarSize(
                            size,
                            spectrumPoints[i].Value,
                            i,
                            barWidth,
                            false,
                            true);
            poligonPoints[cuurentPoligonPointsIndex++] = new PointF(0, (float)(size.Height / 2 + spectrumPoints[0].Value / 2));
            path.AddPolygon(poligonPoints);
            graphics.FillPath(pen.Brush, path);
        }
    }
}
