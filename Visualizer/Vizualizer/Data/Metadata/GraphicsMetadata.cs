using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Visualizer.Data.Metadata
{
    public class GraphicsMetadata
    {
        private static GraphicsMetadata Instance { get; set; }
        public BackgroundStyle CurrentBackgroundStyle { get; set; }
        public int CurrentBackgroundStyleIndex { get; set; }
        public List<BackgroundStyle> BackgroundStyles { get; set; }
        public SmoothingMode SmoothingMode { get; set; }
        public CompositingQuality CompositingQuality { get; set; }
        public PixelOffsetMode PixelOffsetMode { get; set; }
        public static GraphicsMetadata GetInstance()
        {
            if (Instance == null)
                Instance = new GraphicsMetadata();
            return Instance;
        }
        private GraphicsMetadata()=> BackgroundStyles = new List<BackgroundStyle>();
        public void SetCurrentBackgroundStyle(int index)
        {
            CurrentBackgroundStyle = BackgroundStyles[index];
            CurrentBackgroundStyleIndex = index;
        }
        public void SetCurrentBackgroundStyle(string name)=> SetCurrentBackgroundStyle(BackgroundStyles.FindIndex(x => x.Name == name));
    }
}
