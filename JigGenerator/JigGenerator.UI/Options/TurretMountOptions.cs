using System;

namespace JigGenerator.UI.Options
{
    [Serializable]
    public class TurretMountOptions
    {
        public bool MakeJumoAndStandard { get; set; }
        public bool MakeMini { get; set; }

        public TurretMountOptions()
        {
            MakeMini = false;
            MakeJumoAndStandard = true;
        }
    }

    
}
