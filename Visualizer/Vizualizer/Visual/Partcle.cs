using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Visualizer.Helpers;
using Visualizer.Visual;

namespace Vizualizer
{
    public class Particle
    {
        private int Size { get; set; }
        private Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        private int Speed { get; set; }
        private DefaultParticleTypes ParticleType { get; set; }
        private ParticalView ParticleView { get; set; }
        private List<List<PointF>> CustomPoints { get; set; }
        private int MaxX { get; set; }
        private int EmissionPower { get; set; }
        private bool HasEmission { get; set; }
        private int EmissionStep { get; set; }
        private List<Image> ParticleFrames { get; set; }
        private int CurrentFrameIndex { get; set; }
        private int FramesToChange { get; set; }
        private int FramesForOneImage { get; set; } = 4;
        public Particle(int size,
            Color color,
            int x,
            int speedFactor,
            ParticalView particalView,
            List<List<PointF>> points,
            int maxX,
            bool hasEmission,
            int emissionStep,
            int emissionPower)
        {
            Size = size;
            Color = color;
            X = x;
            Speed = size * speedFactor;
            Y = -Speed;
            ParticleView = particalView;
            CustomPoints = points;
            MaxX = maxX;
            HasEmission = hasEmission;
            EmissionStep = emissionStep;
            EmissionPower = emissionPower;
            ParticleFrames = new List<Image>();
            SetupImage();
        }
        public Particle(int size,
            Color color,
            int x,
            int speedFactor,
            ParticalView particalView,
            DefaultParticleTypes particleType,
            int maxX,
            bool hasEmission,
            int emissionStep,
            int emissionPower)
        {
            Size = size;
            Color = color;
            X = x;
            Speed = size * speedFactor;
            Y = -Speed;
            ParticleType = particleType;
            ParticleView = particalView;
            MaxX = maxX;
            HasEmission = hasEmission;
            EmissionStep = emissionStep;
            EmissionPower = emissionPower;
            ParticleFrames = new List<Image>();
            SetupImage();
        }
        public Particle(Particle particle, int x, int speedFactor)
        {
            X = x;
            Size = particle.Size;
            Speed = Size * speedFactor;
            Y = -Speed;
            MaxX = particle.MaxX;
            ParticleFrames = new List<Image>();
            FramesToChange = FramesForOneImage;
            foreach (var item in particle.ParticleFrames)
                ParticleFrames.Add((Image)item.Clone());
        }
        private void SetupImage()
        {
            Bitmap bitmap;
            Graphics graphics;
            var iteration = 0;
            if (CustomPoints == null)
                iteration = 1;
            else
                iteration = CustomPoints.Count;
            for (int i = 0; i < iteration; i++)
            {
                if (HasEmission)
                    bitmap = CreateParticleAndEmission(i);
                else
                {
                    bitmap = new Bitmap(Size, Size);
                    graphics = Graphics.FromImage(bitmap);
                    using (var brush = new SolidBrush(Color))
                        switch (ParticleView)
                        {
                            case ParticalView.Default:
                                switch (ParticleType)
                                {
                                    case DefaultParticleTypes.Rectanlgle:
                                        graphics.FillRectangle(brush, 0, 0, Size, Size);
                                        break;
                                    case DefaultParticleTypes.Ellipse:
                                        graphics.FillEllipse(brush, 0, 0, Size, Size);
                                        break;
                                    case DefaultParticleTypes.Rhombus:
                                        var path = new GraphicsPath();
                                        bitmap = new Bitmap(Size * 2, Size * 2);
                                        graphics = Graphics.FromImage(bitmap);

                                        path.AddLine(
                                            new PointF(Size, 0),
                                            new PointF(0, Size));
                                        path.AddLine(
                                            new PointF(0, Size),
                                            new PointF(Size, 2 * Size));
                                        path.AddLine(
                                            new PointF(Size, 2 * Size),
                                            new PointF(2 * Size, Size));
                                        graphics.FillPath(brush, path);
                                        break;
                                }
                                break;
                            case ParticalView.Custom:
                                {
                                    var path = new GraphicsPath();
                                    var typeToAdd = new PointF[CustomPoints[i].Count];
                                    for (int j = 0; j < CustomPoints[i].Count; j++)
                                        typeToAdd[j] = new PointF(CustomPoints[i][j].X * Size, CustomPoints[i][j].Y * Size);
                                    path.AddPolygon(typeToAdd);
                                    bitmap = new Bitmap((int)path.PathPoints.GetMaxX(), (int)path.PathPoints.GetMaxY());
                                    graphics = Graphics.FromImage(bitmap);
                                    graphics.FillPath(brush, path);
                                    break;
                                }
                        }
                }
                ParticleFrames.Add(bitmap);
            }
        }
        private Bitmap CreateParticleAndEmission(int index)
        {
            Bitmap bitmap;
            GraphicsPath customNotScaledTypePath = new GraphicsPath();
            var matrix = new Matrix();
            var path = new GraphicsPath();
            if (ParticleView == ParticalView.Default && ParticleType == DefaultParticleTypes.Rhombus)
                bitmap = new Bitmap((EmissionStep + 1) * 2 * Size, (EmissionStep + 1) * 2 * Size);
            else if (ParticleView == ParticalView.Custom)
            {
                bitmap = new Bitmap(
                    (int)(CustomPoints[index].GetMaxX() * Size + EmissionPower * 2 * EmissionStep),
                    (int)(CustomPoints[index].GetMaxY() * Size + EmissionPower * 2 * EmissionStep));//(-*Size?)

                customNotScaledTypePath.AddPolygon(CustomPoints[index].ToArray());
            }
            else
                bitmap = new Bitmap(Size + EmissionStep * EmissionPower * 2, Size + EmissionStep * EmissionPower * 2);
            var graphics = Graphics.FromImage(bitmap);
            for (int i = 0; i <= EmissionPower; i++)
            {
                using (var brush = new SolidBrush(
                        Color.FromArgb(
                        (byte)(Color.A / (EmissionPower - i + 1)),
                        Color.R, Color.G, Color.B)))
                    switch (ParticleView)
                    {
                        case ParticalView.Default:
                            switch (ParticleType)
                            {
                                case DefaultParticleTypes.Rectanlgle:
                                    graphics.FillRectangle(brush, i * EmissionStep, i * EmissionStep,
                                        Size + EmissionStep * (EmissionPower - i) * 2,
                                        Size + EmissionStep * (EmissionPower - i) * 2);
                                    break;
                                case DefaultParticleTypes.Ellipse:
                                    graphics.FillEllipse(brush, i * EmissionStep, i * EmissionStep,
                                    Size + EmissionStep * (EmissionPower - i) * 2,
                                    Size + EmissionStep * (EmissionPower - i) * 2);
                                    break;
                                case DefaultParticleTypes.Rhombus:
                                    path.AddLine(
                                        new PointF((EmissionPower + 1) * Size, i * Size),
                                        new PointF(i * Size, (EmissionPower + 1) * Size));
                                    path.AddLine(
                                        new PointF(i * Size, (EmissionPower + 1) * Size),
                                        new PointF((EmissionStep + 1) * Size, ((EmissionPower + 1) * 2 - i) * Size));
                                    path.AddLine(
                                        new PointF((EmissionPower + 1) * Size, ((EmissionPower + 1) * 2 - i) * Size),
                                        new PointF(((EmissionPower + 1) * 2 - i) * Size, (EmissionPower + 1) * Size));
                                    graphics.FillPath(brush, path);
                                    break;
                                default:
                                    throw new Exception();
                            }
                            break;
                        case ParticalView.Custom:
                            {
                                matrix.Reset();
                                var notScalesTypePathClone = customNotScaledTypePath.Clone() as GraphicsPath;
                                matrix.Scale(
                                    Size + (EmissionStep * 2 * (EmissionPower - i) / notScalesTypePathClone.PathPoints.GetMaxX()),
                                    Size + (EmissionStep * 2 * (EmissionPower - i) / notScalesTypePathClone.PathPoints.GetMaxY()),
                                    MatrixOrder.Append);
                                matrix.Translate(EmissionStep * i, EmissionStep * i, MatrixOrder.Append);
                                notScalesTypePathClone.Transform(matrix);
                                graphics.FillPath(brush, notScalesTypePathClone);
                                break;
                            }
                        default:
                            throw new Exception();
                    }
            }
            return bitmap;
        }
        public void Move(double xMove)
        {
            Y += Speed;
            var xChange = (int)(xMove * Speed);
            if (X + xChange > MaxX)
                X = xChange - (MaxX - X);
            else if (X + xChange < 0)
                X = MaxX + (X + xChange);
            else
                X += xChange;
        }
        public Image GetImage()
        {
            var index = CurrentFrameIndex;
            if (FramesToChange-- == 0)
            {
                FramesToChange = FramesForOneImage;
                if (ParticleFrames.Count - 1 == CurrentFrameIndex)
                    CurrentFrameIndex = 0;
                else
                    CurrentFrameIndex++;
            }
            return ParticleFrames[index];
        }
    }
}
