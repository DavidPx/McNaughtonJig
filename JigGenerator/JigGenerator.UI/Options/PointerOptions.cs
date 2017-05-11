using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.UI.Options
{
    [Serializable]
    public class PointerOptions
    {
        public float[] CutterRadii { get; set; }

        public PointerOptions()
        {
            CutterRadii = new float[] { 89, 137, 176, 275 };
        }
    }
}
