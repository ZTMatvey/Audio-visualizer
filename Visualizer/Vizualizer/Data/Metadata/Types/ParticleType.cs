using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualizer.Data.Metadata
{
    public class ParticleType
    {
        public string Name { get; set; }
        public List<List<PointF>> FramePoints { get; set; }
        public int Compression { get; set; }
        public ParticleType()
        {}
        public ParticleType(string name, List<List<PointF>> framePoints, int compression)
        {
            Name = name;
            FramePoints = framePoints;
            Compression = compression;
        }
        public List<List<PointF>> GetCopyFramePoints()
        {
            var result = new List<List<PointF>>();
            for (int i = 0; i < FramePoints.Count; i++)
            {
                result.Add(new List<PointF>(FramePoints[i].Count));
                for (int j = 0; j < FramePoints[i].Count; j++)
                    result[i].Add(FramePoints[i][j]);
            }
            return result;
        }
        public List<List<PointF>> GetCopyFramePointsWithMultiplyOnCompression()
        {
            var result = new List<List<PointF>>();
            for (int i = 0; i < FramePoints.Count; i++)
            {
                result.Add(new List<PointF>(FramePoints[i].Count));
                for (int j = 0; j < FramePoints[i].Count; j++)
                {
                    var point = FramePoints[i][j];
                    result[i].Add(new PointF(point.X * Compression, point.Y * Compression));
                }
            }
            return result;
        }
        public ParticleType(ParticleType particleType)
        {
            Name = particleType.Name;
            FramePoints = new List<List<PointF>>();
            for (int i = 0; i < particleType.FramePoints.Count; i++)
            {
                FramePoints.Add(new List<PointF>(particleType.FramePoints[i].Count));
                for (int j = 0; j < particleType.FramePoints[i].Count; j++)
                    FramePoints[j] = particleType.FramePoints[j];
            }
            Compression = particleType.Compression;
        }
    }
}
