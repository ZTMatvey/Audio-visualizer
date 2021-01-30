using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visualizer.Data.Metadata;
using VizualizerAC;

namespace Visualizer.Visual
{
    public class Metadata
    {
        private static Metadata Instance { get; set; }
        public static Metadata SMetadata { get; set; }
        public int DeviceIndex { get; set; }
        public FieldType FieldType { get; set; }
        public int FPS { get; set; }
        public ThemesController.Themes Theme { get; set; }
        public int FontSize { get; set; }
        public int ItemsFontSize { get; set; }
        public FieldMetadata FieldMetadata { get; set; }
        public ParticleTypes ParticleTypes { get; set; }
        public GraphicsMetadata GraphicsMetadata { get; set; }
        private Metadata()
        {
            SMetadata = this;
            FieldMetadata = FieldMetadata.GetInstance();
            ParticleTypes = ParticleTypes.GetInstance();
            GraphicsMetadata = GraphicsMetadata.GetInstance();
        }
        public static Metadata GetInstance()
        {
            if (Instance == null)
                Instance = new Metadata();
            return Instance;
        }
        public void SetupAfterDesirialization()=> ParticleTypes.SetUpTypes();
        public static void SetMetadata(Metadata metadata)=> Instance = metadata;
    }
}
