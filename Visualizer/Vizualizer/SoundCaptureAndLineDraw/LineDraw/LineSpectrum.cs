using CSCore.DSP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Threading;
using Visualizer.Data.Metadata;
using VizualizerAC;
using static Visualizer.SoundCaptureAndLineDraw.LineDraw.LineDrawer;
using static Visualizer.SoundCaptureAndLineDraw.LineDraw.PointPositionCalculator;

namespace Visualizer.SoundCaptureAndLineDraw.LineDraw
{
    public class LineSpectrum : SpectrumBase
    {
        private int _barCount;
        private double _barSpacing;
        private double _barWidth;
        private double _barHeight;
        public bool withBindingLineSizeToBarSize;
        private int penSize;
        private Size _currentSize;
        public delegate void DrawLineDelegate(Graphics graphics, Pen pen, float[] fftBuffer, Size size);
        private bool IsTwoPoleLines { get; set; }
        public DrawLineDelegate ActualDrawLineStrategy { get; set; }
        public enum DrawLineStrategies
        {
            Bottom,
            Up,
            Left,
            Right,
            BottomAndUp,
            Center,
        }
        public enum ShapeTypes
        {
            Rectangle,
            Line
        }
        private bool IsVerticalBras { get; set; }
        public float MeterOutAverage { get; set; }

        public LineSpectrum(
            FftSize fftSize,
            DrawLineStrategies drawLineStrategy,
            ShapeTypes shapeType,
            bool withBindingLineSizeToBarSize,
            int penSize)
        {
            FftSize = fftSize;
            switch (shapeType)
            {
                case ShapeTypes.Rectangle:
                    switch (drawLineStrategy)
                    {
                        case DrawLineStrategies.Bottom:
                            ActualDrawLineStrategy = CreateSpectrumLineBottomInternal;
                            IsVerticalBras = true;
                            break;
                        case DrawLineStrategies.Up:
                            ActualDrawLineStrategy = CreateSpectrumLineUpInternal;
                            IsVerticalBras = true;
                            break;
                        case DrawLineStrategies.Right:
                            IsVerticalBras = false;
                            ActualDrawLineStrategy = CreateSpectrumLineRightInternal;
                            break;
                        case DrawLineStrategies.Left:
                            ActualDrawLineStrategy = CreateSpectrumLineLeftInternal;
                            IsVerticalBras = false;
                            break;
                        case DrawLineStrategies.BottomAndUp:
                            ActualDrawLineStrategy = CreateSpectrumLineBottomAndUpInternal;
                            IsVerticalBras = true;
                            IsTwoPoleLines = true;
                            break;
                        case DrawLineStrategies.Center:
                            ActualDrawLineStrategy = CreateSpectrumLineCenterInternal;
                            IsVerticalBras = true;
                            break;
                    }
                    break;
                case ShapeTypes.Line:
                    switch (drawLineStrategy)
                    {
                        case DrawLineStrategies.Bottom:
                            ActualDrawLineStrategy = CreateSpectrumLineLineInternal;
                            IsVerticalBras = true;
                            break;
                        case DrawLineStrategies.Up:
                            ActualDrawLineStrategy = CreateSpectrumLineLineUpInternal;
                            IsVerticalBras = true;
                            break;
                        case DrawLineStrategies.Right:
                            break;
                        case DrawLineStrategies.Left:
                            break;
                        case DrawLineStrategies.BottomAndUp:
                            ActualDrawLineStrategy = CreateSpectrumLineLineBottomAndUpInternal;
                            IsVerticalBras = true;
                            break;
                        case DrawLineStrategies.Center:
                            ActualDrawLineStrategy = CreateSpectrumLineLineCentralInternal;
                            IsVerticalBras = true;
                            break;
                    }
                    break;
            }
            this.withBindingLineSizeToBarSize = withBindingLineSizeToBarSize;
            this.penSize = penSize;
        }

        [Browsable(false)]
        public double BarWidth
        {
            get
            {
                return _barWidth;
            }
        }
        [Browsable(false)]
        public double BarHeight
        {
            get
            {
                return _barHeight;
            }
        }
        public double BarSpacing
        {
            get
            {
                return this._barSpacing;
            }
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                this._barSpacing = value;
                this.UpdateFrequencyMapping();
                this.RaisePropertyChanged(nameof(BarSpacing));
                this.RaisePropertyChanged("BarWidth");
            }
        }

        public int BarCount
        {
            get
            {
                return this._barCount;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _barCount = value;
                SpectrumResolution = value;
                UpdateFrequencyMapping();
                RaisePropertyChanged(nameof(BarCount));
                RaisePropertyChanged("BarWidth");
            }
        }

        [Browsable(false)]
        public Size CurrentSize
        {
            get
            {
                return _currentSize;
            }
            protected set
            {
                _currentSize = value;
                RaisePropertyChanged(nameof(CurrentSize));
            }
        }
        public Bitmap CreateSpectrumLine(
          Size size,
          Brush brush,
          GraphicsMetadata gmetadata)
        {
            if (!UpdateFrequencyMappingIfNessesary(size))
                return null;
            float[] fftBuffer = new float[(int)FftSize];
            if (!SpectrumProvider.GetFftData(fftBuffer, this))
                return null;
            using (Pen pen = new Pen(brush,
                withBindingLineSizeToBarSize ?
                    (IsVerticalBras ? (float)_barWidth : (float)_barHeight) :
                    penSize))
            {
                Bitmap bitmap = new Bitmap(size.Width, size.Height);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    PrepareGraphics(graphics, gmetadata);
                    graphics.Clear(Color.Transparent);
                    ActualDrawLineStrategy(graphics, pen, fftBuffer, size);
                }
                return bitmap;
            }
        }
        public Bitmap CreateSpectrumLine(
          Size size,
          List<Color> colors,
          GraphicsMetadata gmetadata)
        {
            if (!UpdateFrequencyMappingIfNessesary(size))
                return (Bitmap)null;

            using (var brush = new LinearGradientBrush(
                new RectangleF(
                    0.0f,
                    0.0f,
                    (float)_barWidth,
                    (float)size.Height),
                Color.White,
                Color.Black,
                LinearGradientMode.Vertical))
            {
                if (colors.Count == 1)
                    colors.Add(colors[0]);
                var cb = new ColorBlend();
                cb.Positions = new float[colors.Count];
                for (int i = 0; i < colors.Count - 1; i++)
                    cb.Positions[i] = (float)i / (colors.Count - 1);
                cb.Positions[cb.Positions.Length - 1] = 1;
                cb.Colors = new Color[colors.Count];
                for (int i = 0; i < colors.Count; i++)
                    cb.Colors[i] = colors[i];
                brush.InterpolationColors = cb;
                brush.RotateTransform(0);

                return CreateSpectrumLine(size, brush, gmetadata);
            }
        }
        private SpectrumPointData[] GetSpectrumPointDatas(Size size)
        {
            float[] fftBuffer = new float[(int)this.FftSize];
            if (!SpectrumProvider.GetFftData(fftBuffer, this))
                return null;
            return CalculateSpectrumPoints(size.Height, fftBuffer);
        }
        public SpectrumPointData[] GetSpectrumPointData(Size size)
        {
            return GetSpectrumPointDatas(size);
        }
        public SpectrumPointData[] LastSpectrumData { get; set; }
        private void CreateSpectrumLineBottomInternal(
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            int height = size.Height;
            SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints((double)height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            for (int index = 0; index < spectrumPoints.Length; ++index)
            {
                SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
                int spectrumPointIndex = spectrumPointData.SpectrumPointIndex;
                double num2;

                if (withBindingLineSizeToBarSize)
                {
                    num2 = BarSpacing * (spectrumPointIndex + 1) + _barWidth * spectrumPointIndex + _barWidth / 2.0;
                    PointF pt1 = new PointF((float)num2, height);
                    PointF pt2 = new PointF((float)num2, (float)(height - spectrumPointData.Value));//todo: тут регулируется наклон
                    graphics.DrawLine(pen, pt1, pt2);
                }
                else
                {
                    var point = CalculatePositionForBottomStateWithoutBindingLineSizeToBarSize(size, spectrumPointData.Value, index, BarWidth);
                    var rectangle = new Rectangle(point, new Size((int)_barWidth, height));
                    graphics.DrawRectangle(pen, rectangle);
                }
            }
        }
        private void CreateSpectrumLineUpInternal(
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            int height = size.Height;
            SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints((double)height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            for (int index = 0; index < spectrumPoints.Length; ++index)
            {
                SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
                int spectrumPointIndex = spectrumPointData.SpectrumPointIndex;
                double num2;
                if (withBindingLineSizeToBarSize)
                {
                    num2 = BarSpacing * (spectrumPointIndex + 1) + _barWidth * spectrumPointIndex + _barWidth / 2.0;
                    PointF pt1 = new PointF((float)num2, 0);
                    PointF pt2 = new PointF((float)num2, (float)(spectrumPointData.Value));
                    graphics.DrawLine(pen, pt1, pt2);
                }
                else
                {
                    var point = CalculateYZeroPositionForUpStateWithoutBindingLineSizeToBarSize(index, BarWidth);
                    var rectangle = new Rectangle(point, new Size((int)_barWidth, (int)spectrumPointData.Value));
                    graphics.DrawRectangle(pen, rectangle);
                }
            }
        }
        private void CreateSpectrumLineLeftInternal(
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            int width = size.Width;
            SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints((double)width, fftBuffer);
            LastSpectrumData = spectrumPoints;
            float num1 = 0.0f;

            for (int index = 0; index < spectrumPoints.Length; ++index)
            {
                SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
                double num2;

                if (withBindingLineSizeToBarSize)
                {
                    num2 = this.BarSpacing * (index + 1) + BarHeight * index + BarHeight / 2.0;
                    PointF pt1 = new PointF(0, (float)num2);
                    PointF pt2 = new PointF((float)spectrumPointData.Value, (float)num2);//todo: тут регулируется наклон
                    graphics.DrawLine(pen, pt1, pt2);
                }
                else
                {
                    num2 = BarHeight * index;
                    var rectangle = new Rectangle(new System.Drawing.Point(0, (int)num2), new Size((int)spectrumPointData.Value, (int)_barHeight));
                    graphics.DrawRectangle(pen, rectangle);
                }

                num1 += (int)spectrumPointData.Value;
            }
            MeterOutAverage = (float)((width - num1 / spectrumPoints.Length) / width * 100.0);
        }
        private void CreateSpectrumLineRightInternal(//todo тутуту
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            int width = size.Width;
            SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints((double)width, fftBuffer);
            LastSpectrumData = spectrumPoints;
            float num1 = 0.0f;

            for (int index = 0; index < spectrumPoints.Length; ++index)
            {
                SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
                double num2;

                if (withBindingLineSizeToBarSize)
                {
                    num2 = BarSpacing * (index + 1) + BarHeight * index + BarHeight / 2.0;
                    PointF pt1 = new PointF(width, (float)num2);
                    PointF pt2 = new PointF(width - (float)spectrumPointData.Value, (float)num2);//todo: тут регулируется наклон
                    graphics.DrawLine(pen, pt1, pt2);
                }
                else
                {
                    num2 = BarHeight * index;
                    var rectangle = new Rectangle(new System.Drawing.Point(width - (int)spectrumPointData.Value, (int)num2), new Size(width, (int)_barHeight));
                    graphics.DrawRectangle(pen, rectangle);
                }

                num1 += (int)spectrumPointData.Value;
            }
            MeterOutAverage = (float)((width - num1 / spectrumPoints.Length) / width * 100.0);
        }
        private void CreateSpectrumLineBottomAndUpInternal(//todo тутуту
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            int height = size.Height;
            SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints((double)height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            for (int index = 0; index < spectrumPoints.Length / 2; ++index)
            {
                SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
                spectrumPointData.Value /= 2;
                int spectrumPointIndex = spectrumPointData.SpectrumPointIndex;


                double num2;

                if (withBindingLineSizeToBarSize)
                {
                    num2 = BarSpacing * (spectrumPointIndex + 1) + _barWidth * spectrumPointIndex + _barWidth / 2.0;
                    PointF pt1 = new PointF((float)num2, height);
                    PointF pt2 = new PointF((float)num2, (float)(height - spectrumPointData.Value));//todo: тут регулируется наклон
                    graphics.DrawLine(pen, pt1, pt2);
                }
                else
                {
                    var point = CalculatePositionForBottomStateWithoutBindingLineSizeToBarSize(size, spectrumPointData.Value, index, BarWidth);
                    var rectangle = new Rectangle(point, new Size((int)_barWidth, height));
                    graphics.DrawRectangle(pen, rectangle);
                }
            }
            for (int index = 0; index < spectrumPoints.Length / 2; ++index)
            {
                SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index + spectrumPoints.Length / 2];
                spectrumPointData.Value /= 2;


                double num2;
                if (withBindingLineSizeToBarSize)
                {
                    num2 = BarSpacing * (index + 1) + _barWidth * index + _barWidth / 2.0;
                    PointF pt1 = new PointF((float)num2, 0);
                    PointF pt2 = new PointF((float)num2, (float)(spectrumPointData.Value));//todo: тут регулируется наклон
                    graphics.DrawLine(pen, pt1, pt2);
                }
                else
                {
                    var point = CalculateYZeroPositionForUpStateWithoutBindingLineSizeToBarSize(index, BarWidth);
                    var rectangle = new Rectangle(point, new Size((int)_barWidth, (int)spectrumPointData.Value));
                    graphics.DrawRectangle(pen, rectangle);
                }
            }
        }
        private void CreateSpectrumLineCenterInternal(
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            int height = size.Height;
            SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints((double)height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            for (int index = 0; index < spectrumPoints.Length; ++index)
            {
                SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
                int spectrumPointIndex = spectrumPointData.SpectrumPointIndex;//todo отследить размер, чтобы на экранх 100000х1000000 было норм
                if (spectrumPointData.Value == 0)
                    spectrumPointData.Value = 1;
                double num2;

                if (withBindingLineSizeToBarSize)
                {
                    num2 = BarSpacing * (spectrumPointIndex + 1) + _barWidth * spectrumPointIndex + _barWidth / 2.0;
                    PointF pt1 = new PointF((float)num2, (float)((height - spectrumPointData.Value) / 2));
                    PointF pt2 = new PointF((float)num2, (float)(height - spectrumPointData.Value));//todo: тут регулируется наклон
                    graphics.DrawLine(pen, pt1, pt2);
                }
                else
                {
                    var point = CalculateYZeroPositionForCenterStateWithoutBindingLineSizeToBarSize(size, spectrumPointData.Value, index, BarWidth, false, false);
                    var rectangle = new Rectangle(new System.Drawing.Point((int)point.X, (int)point.Y),
                        new Size((int)_barWidth, height - (int)((height - spectrumPointData.Value))));
                    graphics.DrawRectangle(pen, rectangle);
                }
            }
        }
        private void CreateSpectrumLineLineInternal(
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            SpectrumBase.SpectrumPointData[] spectrumPoints = CalculateSpectrumPoints(size.Height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            DrawBottomLines(graphics, pen, spectrumPoints, size, BarWidth, withBindingLineSizeToBarSize);
        }
        private void CreateSpectrumLineLineUpInternal(
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            SpectrumPointData[] spectrumPoints = CalculateSpectrumPoints(size.Height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            DrawUpLines(graphics, pen, spectrumPoints, size, BarWidth, withBindingLineSizeToBarSize);
        }
        private void CreateSpectrumLineLineBottomAndUpInternal(//todo тутуту
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            SpectrumPointData[] spectrumPoints = CalculateSpectrumPoints(size.Height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            DrawBottomAndUp(graphics, pen, spectrumPoints, size, BarWidth, withBindingLineSizeToBarSize);
        }
        private void CreateSpectrumLineLineCentralInternal(
          Graphics graphics,
          Pen pen,
          float[] fftBuffer,
          Size size)
        {
            SpectrumBase.SpectrumPointData[] spectrumPoints = CalculateSpectrumPoints(size.Height, fftBuffer);
            LastSpectrumData = spectrumPoints;

            DrawCenter(graphics, pen, spectrumPoints, size, BarWidth, withBindingLineSizeToBarSize);
        }
        protected override void UpdateFrequencyMapping()
        {
            _barWidth = Math.Max((_currentSize.Width - BarSpacing * BarCount + 1) / BarCount, 1E-05);
            _barHeight = Math.Max((_currentSize.Height - BarSpacing * BarCount + 1) / BarCount, 1E-05);//todo вынести в отдеальный метод
            if (IsTwoPoleLines)
            {
                _barWidth *= 2;
                _barHeight *= 2;
            }
            base.UpdateFrequencyMapping();
        }

        private bool UpdateFrequencyMappingIfNessesary(Size newSize)
        {
            if (newSize != this.CurrentSize)
            {
                CurrentSize = newSize;
                UpdateFrequencyMapping();
            }
            return newSize.Width > 0 && newSize.Height > 0;
        }

        private void PrepareGraphics(Graphics graphics, GraphicsMetadata gmetadata)
        {
            graphics.SmoothingMode = gmetadata.SmoothingMode;
            graphics.CompositingQuality = gmetadata.CompositingQuality;
            graphics.PixelOffsetMode = gmetadata.PixelOffsetMode;
        }
    }
}
