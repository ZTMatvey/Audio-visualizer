using System.Drawing;
using VizualizerAC;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Visualizer.SoundCaptureAndLineDraw.LineDraw;

namespace Visualizer.Visual
{
    public static class MetadataSerializator
    {
        private static string PathToZTMFolder = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ZTM";
        private static string PathToZTMVisualizerFolder = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ZTM\Visualizer";
        private static string PathToMetadata = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ZTM/Visualizer\Metadata.json";
        public static void Serialize()
        {
            CheckDirectories();
            if (File.Exists(PathToMetadata))
                SerializeMetadata();
            else
            {
                File.Create(PathToMetadata).Dispose();
                SerializeMetadata();
            }
        }
        private static void SerializeMetadata()
        {
            var json = JsonConvert.SerializeObject(Metadata.SMetadata);
            try
            {
                var saveThread = new Thread(new ThreadStart(() =>
                {
                    using (var sw = new StreamWriter(PathToMetadata, false))
                        sw.Write(json);
                }));
                saveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void SetDefaultMetadata()
        {
            Metadata.SMetadata.DeviceIndex = 0;
            Metadata.SMetadata.FieldType = FieldType.Custom;
            Metadata.SMetadata.FPS = 35;
            Metadata.SMetadata.FieldMetadata.CustomStyles = new List<Data.Metadata.CustomStyle>();
            Metadata.SMetadata.FieldMetadata.CustomStyles.Add(new Data.Metadata.CustomStyle() { Name = "Default", Colors = new List<Color>() { Color.White, Color.Black} });
            Metadata.SMetadata.FieldMetadata.SelectedStyle = 0;
            Metadata.SMetadata.FieldMetadata.DrawLineStrategy = LineSpectrum.DrawLineStrategies.Bottom;
            Metadata.SMetadata.FieldMetadata.ShapeType = LineSpectrum.ShapeTypes.Rectangle;
            Metadata.SMetadata.FieldMetadata.ScalingStrategy = ScalingStrategy.Decibel;
            Metadata.SMetadata.FieldMetadata.ShouldShowParticles = true;
            Metadata.SMetadata.FieldMetadata.MaxParticlesCount = 150;
            Metadata.SMetadata.FieldMetadata.MinParticlesSpeed = 10;
            Metadata.SMetadata.FieldMetadata.MaxParticlesSpeed = 25;
            Metadata.SMetadata.FieldMetadata.MinParticlesSize = 5;
            Metadata.SMetadata.FieldMetadata.MaxParticlesSize = 35;
            Metadata.SMetadata.FontSize = 25;
            Metadata.SMetadata.FieldMetadata.IsWind = true;
            Metadata.SMetadata.FieldMetadata.ChanceForWind = 75;
            Metadata.SMetadata.FieldMetadata.MaxWindForce = 15;
            Metadata.SMetadata.FieldMetadata.MinWindForce = 5;
            Metadata.SMetadata.FieldMetadata.MaxWindTime = 50;
            Metadata.SMetadata.FieldMetadata.MinWindTime = 25;
            Metadata.SMetadata.Theme = ThemesController.Themes.Dark;
            Metadata.SMetadata.ItemsFontSize = 25;
            Metadata.SMetadata.FieldMetadata.LineCount = 90;
            Metadata.SMetadata.GraphicsMetadata.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
            Metadata.SMetadata.GraphicsMetadata.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            Metadata.SMetadata.GraphicsMetadata.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Metadata.SMetadata.GraphicsMetadata.BackgroundStyles.Add(new Data.Metadata.BackgroundStyle("Default"));
            Metadata.SMetadata.GraphicsMetadata.SetCurrentBackgroundStyle("Default");
        }
        private static void ValidateValues()
        {
            if (Metadata.SMetadata.ItemsFontSize <= 0)
                Metadata.SMetadata.ItemsFontSize = 25;
            if (Metadata.SMetadata.FontSize <= 0)
                Metadata.SMetadata.FontSize = 25;
            if (Metadata.SMetadata.FieldMetadata.LineCount <= 0)
                Metadata.SMetadata.FieldMetadata.LineCount = 90;
        }
        public static void DesirializeOrSetDefault()
        {
            CheckDirectories();
            if (File.Exists(PathToMetadata))
            {
                var json = "";
                using (var stream = new FileStream(PathToMetadata, FileMode.Open))
                using (var sr = new StreamReader(stream))
                    json = sr.ReadToEnd();
                try
                {
                    var md = JsonConvert.DeserializeObject<Metadata>(json);
                    Metadata.SetMetadata(md);
                    ValidateValues();
                }
                catch(Exception ex)
                {
                    DeleteMetadataFile();
                    SetDefaultMetadata();
                    MessageBox.Show($"Ошибка при десириализации. Файл метаданных был удален.\n({ex.Message})");
                }
            }
            else
                SetDefaultMetadata();
        }
        public static void DeleteMetadataFile() => File.Delete(PathToMetadata);
        private static void CheckDirectories()
        {
            if (!Directory.Exists(PathToZTMFolder))
                Directory.CreateDirectory(PathToZTMFolder);
            if (!Directory.Exists(PathToZTMVisualizerFolder))
                Directory.CreateDirectory(PathToZTMVisualizerFolder);
        }
    }
}
