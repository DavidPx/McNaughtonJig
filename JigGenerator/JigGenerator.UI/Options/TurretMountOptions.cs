using System;
using System.ComponentModel;
using System.Windows;

namespace JigGenerator.UI.Options
{
    [Serializable]
    public class TurretMountOptions : DependencyObject
    {
        public float PostGap { get; set; }
        public string Label { get; set; }
        public CutterSize CutterSize { get; set; }
        
        public bool IncludeInDrawing
        {
            get => (bool)GetValue(IncludeInDrawingProperty);
            set => SetValue(IncludeInDrawingProperty, value);
        }

        // Using a DependencyProperty as the backing store for IncludeInDrawing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncludeInDrawingProperty =
            DependencyProperty.Register("IncludeInDrawing", typeof(bool?), typeof(TurretMountOptions), new PropertyMetadata(false));
    }    

    public enum CutterSize
    {
        Large,
        Standard,
        Small,
        Mini
    }
}
