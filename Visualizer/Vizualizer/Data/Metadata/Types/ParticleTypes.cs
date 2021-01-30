using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Visualizer.Data.Metadata;

namespace Visualizer.Visual
{
    public enum ParticalView
    {
        Default,
        Custom
    }
    public enum DefaultParticleTypes
    {
        Rectanlgle,
        Ellipse,
        Rhombus
    }
    public class ParticleTypes
    {
        private static ParticleTypes Instance { get; set; }
        public static ParticleTypes GetInstance()
        {
            if (Instance == null)
                Instance = new ParticleTypes();
            return Instance;
        }
        private ParticleTypes()
        {
            UsersTypes = new List<ParticleType>();
            TypeNames = new List<string>();
        }
        public void SetUpTypes()
        {
            bool isExist;
            foreach (var item in Enum.GetValues(typeof(DefaultParticleTypes)))
            {
                isExist = false;
                foreach (var Sitem in TypeNames)
                    if (item.ToString() == Sitem)
                    {
                        isExist = true;
                        break;
                    }
                if (!isExist)
                    TypeNames.Add(item.ToString());
            }
        }
        public List<string> TypeNames { get; set; }
        public List<ParticleType> UsersTypes { get; set; }
        public void AddUserType(string name, List<List<PointF>> points, int compression, bool overwrite = false)
        {
            if (!overwrite)
            {
                foreach (var item in UsersTypes)
                    if (item.Name == name)
                        throw new Exception("Частица с таким именем уже существует");

                var pointsToAdd = new List<List<PointF>>();
                for (int i = 0; i < points.Count; i++)
                {
                    pointsToAdd.Add(new List<PointF>());
                    for (int j = 0; j < points[i].Count; j++)
                    {
                        var point = points[i][j];
                        pointsToAdd[i].Add(new PointF(point.X / compression, point.Y / compression));
                    }
                }
                UsersTypes.Add(new ParticleType(name, pointsToAdd, compression));
                TypeNames.Add(name);
            }
            else
            {
                var index = 0;
                for (int i = 0; i < UsersTypes.Count; i++)
                    if (UsersTypes[i].Name == name)
                    {
                        index = i;
                        break;
                    }
                UsersTypes[index].Compression = compression;

                var pointsToAdd = new List<List<PointF>>();
                for (int i = 0; i < points.Count; i++)
                {
                    pointsToAdd.Add(new List<PointF>());
                    for (int j = 0; j < points[i].Count; j++)
                    {
                        var point = points[i][j];
                        pointsToAdd[i].Add(new PointF(point.X / compression, point.Y / compression));
                    }
                }
                UsersTypes[index].FramePoints = pointsToAdd;
            }
        }
        public void RemoveTypeByName(string name)
        {
            for (int i = 0; i < UsersTypes.Count; i++)
                if (UsersTypes[i].Name == name)
                {
                    UsersTypes.RemoveAt(i);
                    break;
                }
            for (int i = 0; i < TypeNames.Count; i++)
                if (TypeNames[i] == name)
                {
                    TypeNames.RemoveAt(i);
                    break;
                }
        }
        public List<List<PointF>> GetPoints(string typeName)
        {
            var item = UsersTypes.Where(x => x.Name == typeName).ToList();
            return item[0].FramePoints;
        }
        public List<List<PointF>> GetPoints(int index)
        {
            return GetPoints(TypeNames[index]);
        }
        public string[] GetTypeNames()
        {
            return TypeNames.ToArray();
        }
        public bool IsUserType(string typeName)
        {
            if (UsersTypes.Select(x => x.Name == typeName) != null)
                return true;
            return false;
        }
    }
}
