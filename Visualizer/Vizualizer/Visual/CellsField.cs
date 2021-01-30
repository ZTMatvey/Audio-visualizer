using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visualizer.Visual;
using VizualizerAC;

namespace Vizualizer
{
    public class CellsField
    {
        private List<Color> Colors { get; set; }
        private List<Particle> Cells { get; set; }
        public int MaxCells { get; set; }
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        private Random BlessRNG { get; set; }
        public Bitmap Field { get; set; }
        public int MaxSpeed { get; set; }
        public int MinSpeed { get; set; }
        private Graphics Graphics { get; set; }
        private const int Step = 5;
        private List<Particle> ParticlePrefabs { get; set; }
        private bool ViaPrefabs { get; set; }
        private int PrefabsCount { get; set; }
        public Size ScreenSize { get; set; }
        public bool IsWind { get; set; }
        public double MinWindForce { get; set; }
        public double MaxWindForce { get; set; }
        private double CurrentMaxWindForce { get; set; }
        private double CurrentWindForce { get; set; }
        private bool IsWindNow { get; set; }
        public int MaxWindTime { get; set; }
        public int MinWindTime { get; set; }
        private int CurrentMaxWindTime { get; set; }
        public int ChanceForWind { get; set; }
        private bool PositiveWind { get; set; }
        private int CurrentWindTime { get; set; }
        private bool IsStarting { get; set; }
        private bool IsStopping { get; set; }
        public double TempOfIncreaseAndDecrease { get; set; }
        public int AverageParticlesInOneStep { get; set; }
        public int QualityOfColors { get; set; } = 10;
        public int SpeedFactor { get; set; }
        public CellsField(Size fieldSize, bool ColorBetweenTwoColors, List<Color> colors, bool viaPrefabs, int prefabsCount)
        {
            Colors = new List<Color>();
            BlessRNG = new Random();
            Cells = new List<Particle>();
            if (colors.Count == 1)
                Colors.Add(colors[0]);
            else
            {
                byte A, R, G, B;
                for (int i = 0; i < colors.Count - 1; i++)
                    for (int j = 1; j < QualityOfColors; j++)
                    {
                        A = (byte)(((double)(QualityOfColors - j) / QualityOfColors * colors[i].A) + colors[i + 1].A * ((double)j/QualityOfColors));
                        R = (byte)(((double)(QualityOfColors - j) / QualityOfColors * colors[i].R) + colors[i + 1].R * ((double)j/QualityOfColors));
                        G = (byte)(((double)(QualityOfColors - j) / QualityOfColors * colors[i].G) + colors[i + 1].G * ((double)j/QualityOfColors));
                        B = (byte)(((double)(QualityOfColors - j) / QualityOfColors * colors[i].B) + colors[i + 1].B * ((double)j/QualityOfColors));
                        Colors.Add(Color.FromArgb(A, R, G, B));
                    }
            }
            Field = new Bitmap(fieldSize.Width, fieldSize.Height);
            Graphics = Graphics.FromImage(Field);
            ScreenSize = fieldSize;
            PrefabsCount = prefabsCount;
            ViaPrefabs = viaPrefabs;
        }
        public void SetupParticles()
        {
            SpeedFactor = BlessRNG.Next(MinSpeed, MaxSpeed);
            if (ViaPrefabs)
            {
                ParticlePrefabs = new List<Particle>(PrefabsCount);
                SetupParticlePrefabs();
            }
        }
        private void SetupParticlePrefabs()
        {
            for (int i = 0; i < PrefabsCount; i++)
                if (Metadata.SMetadata.FieldMetadata.ParticalView == ParticalView.Custom)
                    ParticlePrefabs.Add(new Particle(
                    BlessRNG.Next(
                        Metadata.SMetadata.FieldMetadata.MinParticlesSize,
                        Metadata.SMetadata.FieldMetadata.MaxParticlesSize),
                    Colors[BlessRNG.Next(0, Colors.Count)],
                    Field.Width - MaxSize < 0 ? 0 : BlessRNG.Next(0, Field.Width - MaxSize),
                    SpeedFactor,
                    Metadata.SMetadata.FieldMetadata.ParticalView,
                    Metadata.SMetadata.FieldMetadata.CustomType,
                    ScreenSize.Width,
                    Metadata.SMetadata.FieldMetadata.HasEmission,
                    Metadata.SMetadata.FieldMetadata.EmissionStep,
                    Metadata.SMetadata.FieldMetadata.EmissionPower));
                else
                    ParticlePrefabs.Add(new Particle(
                    BlessRNG.Next(
                        Metadata.SMetadata.FieldMetadata.MinParticlesSize,
                        Metadata.SMetadata.FieldMetadata.MaxParticlesSize),
                    Colors[BlessRNG.Next(0, Colors.Count)],
                    Field.Width - MaxSize < 0 ? 0 : BlessRNG.Next(0, Field.Width - MaxSize),
                    SpeedFactor,
                    Metadata.SMetadata.FieldMetadata.ParticalView,
                    Metadata.SMetadata.FieldMetadata.DefParticalType,
                    ScreenSize.Width,
                    Metadata.SMetadata.FieldMetadata.HasEmission,
                    Metadata.SMetadata.FieldMetadata.EmissionStep,
                    Metadata.SMetadata.FieldMetadata.EmissionPower));
        }
        public void AddCells(int count)
        {
            for (int i = 0; i < count; i++)
                if (Cells.Count >= MaxCells)
                    break;
                else
                {
                    Cells.Add(new Particle(
                        ParticlePrefabs[BlessRNG.Next(0, ParticlePrefabs.Count)],
                        Field.Width - MaxSize < 0 ? 0 : BlessRNG.Next(0, Field.Width - MaxSize),
                        SpeedFactor));
                }
        }
        public void Move()
        {
            foreach (var item in Cells)
                item.Move(PositiveWind ? CurrentWindForce : -CurrentWindForce);
        }
        public void MakeStep()
        {
            Move();
            if (IsWind)
            {
                if (!IsWindNow)
                {
                    if (ChanceForWind < BlessRNG.Next(1, 100))
                    {
                        IsWindNow = true;
                        IsStarting = true;
                        CurrentMaxWindForce = BlessRNG.NextDouble() * (MaxWindForce - MinWindForce) + MinWindForce;
                        CurrentMaxWindTime = BlessRNG.Next(MinWindTime, MaxWindTime);
                        PositiveWind = BlessRNG.Next(0, 2) == 0;
                        CurrentWindTime = 0;
                        CurrentWindForce = 0;
                    }
                }
                else if (!IsStarting && IsWindNow && ++CurrentWindTime >= CurrentMaxWindTime)
                    IsStopping = true;
                if (IsStarting)
                    if ((CurrentWindForce += TempOfIncreaseAndDecrease) >= CurrentMaxWindForce)
                    {
                        if (CurrentWindForce > CurrentMaxWindForce)
                            CurrentWindForce = CurrentMaxWindForce;
                        IsStarting = false;
                    }
                if (IsStopping)
                    if ((CurrentWindForce -= TempOfIncreaseAndDecrease) <= 0)
                    {
                        if (CurrentWindForce < 0)
                            CurrentWindForce = 0;
                        IsWindNow = false;
                        IsStopping = false;
                    }
            }
        }
        public System.Windows.Media.ImageSource GetField()
        {

            Graphics.Clear(Color.Transparent);
            for (int i = 0; i < Cells.Count; i++)
                if (Cells[i].Y > Field.Height)
                    Cells.RemoveAt(i);
            foreach (var item in Cells)
                Graphics.DrawImage(item.GetImage(), new PointF(item.X, item.Y));
            return Field.ImageSourceForBitmap();
        }
    }
}
