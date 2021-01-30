using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visualizer.SoundCaptureAndLineDraw.LineDraw;
using Visualizer.Visual;
using VizualizerAC;

namespace Visualizer.Data.Metadata
{
    public class FieldMetadata
    {
        private static FieldMetadata Instance { get; set; }
        public static FieldMetadata GetInstance()
        {
            if (Instance == null)
                Instance = new FieldMetadata();
            return Instance;
        }
        private FieldMetadata()
        {}
        public List<CustomStyle> CustomStyles { get; set; }
        public int SelectedStyle { get; set; }
        public int CurrentTypeIndex { get; set; }
        public LineSpectrum.DrawLineStrategies DrawLineStrategy { get; set; }
        public LineSpectrum.ShapeTypes ShapeType { get; set; }
        public int PenSize { get; set; }
        public bool BindingPenSizeToLineSize { get; set; }
        public ScalingStrategy ScalingStrategy { get; set; }
        public bool ShouldShowParticles { get; set; }
        public int MaxParticlesCount { get; set; }
        public int MinParticlesSpeed { get; set; }
        public int MaxParticlesSpeed { get; set; }
        public int MinParticlesSize { get; set; }
        public int MaxParticlesSize { get; set; }
        public ParticalView ParticalView { get; set; }
        public DefaultParticleTypes DefParticalType { get; set; }
        public List<List<PointF>> CustomType { get; set; }
        public bool IsWind { get; set; }
        public int MaxWindTime { get; set; }
        public int MinWindTime { get; set; }
        public double MaxWindForce { get; set; }
        public double MinWindForce { get; set; }
        public int ChanceForWind { get; set; }
        public int LineCount { get; set; }
        public bool HasEmission { get; set; }
        public int EmissionPower { get; set; }
        public int EmissionStep { get; set; }
    }
}
