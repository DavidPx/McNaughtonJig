using System;

namespace JigGenerator.UI.Options
{
    /// <summary>
    /// All the parmaeters that go into a jig.  Not used for other settings that are specific to the program.
    /// </summary>
    [Serializable]
    public class JigOptions
    {
        public float FastenerDiameter { get; set; }

        public ProtractorOptions Protractor { get; set; }

        public PointerOptions Pointer { get; set; }

        public DrawingOptions Drawing { get; set; }

        public TurretMountOptions TurretMount { get; set; }
        
        public JigOptions()
        {
            FastenerDiameter = 6f;
            Protractor = new ProtractorOptions();
            Pointer = new PointerOptions();
            Drawing = new DrawingOptions();
            TurretMount = new TurretMountOptions();
        }
    }
}
