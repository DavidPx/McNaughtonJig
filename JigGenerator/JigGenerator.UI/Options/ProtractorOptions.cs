using System;

namespace JigGenerator.UI.Options
{
    [Serializable]
    public class ProtractorOptions
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
