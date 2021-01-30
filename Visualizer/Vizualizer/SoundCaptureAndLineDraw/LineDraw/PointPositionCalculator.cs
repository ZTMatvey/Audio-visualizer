using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualizer.SoundCaptureAndLineDraw.LineDraw
{
    public static class PointPositionCalculator
    {
        public static PointF CalculatePositionForBottomStateWithoutBindingLineSizeToBarSizeF(
            Size size,
            double spectrumPointValue,
            int index,
            double barWidth)
        {
            return new PointF((float)(index * barWidth), (float)(size.Height - spectrumPointValue));
        }
        public static Point CalculatePositionForBottomStateWithoutBindingLineSizeToBarSize(
            Size size,
            double spectrumPointValue,
            int index,
            double barWidth)
        {
            return new Point((int)(index * barWidth), (int)(size.Height - spectrumPointValue));
        }
        public static PointF CalculatePositionForUpStateWithoutBindingLineSizeToBarSizeF(
            double spectrumPointValue,
            int index,
            double barWidth)
        {
            return new PointF((float)(index * barWidth), (float)spectrumPointValue);
        }
        public static Point CalculateYZeroPositionForUpStateWithoutBindingLineSizeToBarSize(
            int index,
            double barWidth)
        {
            return new Point((int)(index * barWidth), 0);
        }
        public static PointF CalculateYZeroPositionForCenterStateWithoutBindingLineSizeToBarSize(
            Size size,
            double spectrumPointValue,
            int index,
            double barWidth,
            bool isLast = false,
            bool withPlus = true)
        {
            var centerY = size.Height / 2;
            float Y;
            if (withPlus)
                Y = (float)(centerY + spectrumPointValue / 2);
            else
                Y = (float)(centerY - spectrumPointValue / 2);
            Y = isLast ? centerY : Y;
            return new PointF((int)(index * barWidth), Y);
        }
    }
}
