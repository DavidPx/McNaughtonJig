using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.UI.Options
{
    [Serializable]
    internal class RootOptions
    {
        public float FastenerDiameter { get; set; }

        public ProtractorOptions Protractor { get; set; }

        public PointerOptions Pointer { get; set; }

        public DrawingOptions Drawing { get; set; }

        public TurretMountOptions TurretMount { get; set; }

        public RootOptions()
        {
            FastenerDiameter = 6f;
            Protractor = new ProtractorOptions();
            Pointer = new PointerOptions();
            Drawing = new DrawingOptions();
            TurretMount = new TurretMountOptions();
        }
    }
}
