using System;

namespace JigGenerator.UI.Options
{
    [Serializable]
    public class TurretMountOptions
    {
        public float PostGap { get; set; }
        public string Label { get; set; }
        public CutterSize CutterSize { get; set; }
        public bool IncludeInDrawing { get; set; }
    }    

    public enum CutterSize
    {
        Large,
        Standard,
        Small,
        Mini
    }
}
