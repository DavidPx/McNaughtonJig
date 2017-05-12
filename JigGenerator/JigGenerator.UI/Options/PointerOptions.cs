using System;

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
