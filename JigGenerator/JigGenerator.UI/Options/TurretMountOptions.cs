using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.UI.Options
{
    [Serializable]
    internal class TurretMountOptions
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
