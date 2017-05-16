using System;
using System.Collections.Generic;

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

        public IList<TurretMountOptions> TurretMounts { get; set; }
        
        public JigOptions()
        {
            FastenerDiameter = 6f;
            Protractor = new ProtractorOptions();
            Pointer = new PointerOptions();
            Drawing = new DrawingOptions();
            TurretMounts = new List<TurretMountOptions>
            {
                new TurretMountOptions
                {
                    CutterSize = CutterSize.Mini,
                    IncludeInDrawing = false,
                    Label = "Mini",
                    PostGap = 3.26f
                },
                new TurretMountOptions
                {
                    CutterSize = CutterSize.Small,
                    IncludeInDrawing = false,
                    Label = "Small",
                    PostGap = 5.17f
                },
                new TurretMountOptions
                {
                    CutterSize = CutterSize.Standard,
                    IncludeInDrawing = true,
                    Label = "Standard",
                    PostGap = 6f
                },
                new TurretMountOptions
                {
                    CutterSize = CutterSize.Large,
                    IncludeInDrawing = true,
                    Label = "Large",
                    PostGap = 5.94f
                }
            };
        }
    }
}
