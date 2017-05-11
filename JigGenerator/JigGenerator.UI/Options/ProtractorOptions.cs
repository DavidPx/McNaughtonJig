using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.UI.Options
{
    [Serializable]
    internal class ProtractorOptions
    {
        public float ArmLength { get; set; }
        public ushort MajorDivisions { get; set; }
        public ushort MinorDivisions { get; set; }

        public ProtractorOptions()
        {
            ArmLength = 100f;
            MajorDivisions = 12;
            MinorDivisions = 10;
        }
    }
}
