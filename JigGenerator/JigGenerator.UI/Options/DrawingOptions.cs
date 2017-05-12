using System;

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
