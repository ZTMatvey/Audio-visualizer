using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualizer.Data.Metadata
{
    public class CustomStyle
    {
        public CustomStyle(bool addDefaultColor = true)
        {
            Colors = new List<Color>();
            if (addDefaultColor)
                Colors.Add(Color.Black);
        }
        public string Name { get; set; }
        public List<Color> Colors { get; set; }
        public override string ToString() => Name;
        public CustomStyle Clone()
        {
            var resultStyle = new CustomStyle(false)
            {
                Name = this.Name
            };
            foreach (var color in Colors)
                resultStyle.Colors.Add(color);
            return resultStyle;
        }
    }
}
