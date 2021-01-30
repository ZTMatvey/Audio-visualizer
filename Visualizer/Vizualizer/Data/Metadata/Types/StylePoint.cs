using System.Windows.Media;

namespace Visualizer.Data.Metadata
{
    public class StylePoint
    {
        public StylePoint()
        {}
        public StylePoint(double offset, Color color)
        {
            Offset = offset;
            Color = color;
        }
        public double Offset { get; set; }
        public Color Color { get; set; }
        public StylePoint Clone() => new StylePoint(Offset, Color);
    }
}
