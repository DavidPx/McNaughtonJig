using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.UI.Options
{
    [Serializable]
    public class DrawingOptions
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public DrawingOptions()
        {
            Width = Height = 181;
        }
    }
}
