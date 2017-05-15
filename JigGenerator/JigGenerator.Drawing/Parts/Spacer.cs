using Svg;
using JigGenerator.Drawing.Primitives;

namespace JigGenerator.Drawing.Parts
{
    public class Spacer : Part
    {
        private static int idCounter = 0;
        protected string Label;
        protected string ContourPath { get; set; }
        
        public Spacer(float fastenerDiameter, string label)
            : base(fastenerDiameter)
        {
            this.Label = label;
            
            // This is an ellipse
            ContourPath = "M 89.035347,170.47115 A 89.035347,196.47765 0 0 1 0,366.94879 89.035347,196.47765 0 0 1 -89.035347,170.47115 89.035347,196.47765 0 0 1 0,-26.0065 89.035347,196.47765 0 0 1 89.035347,170.47115 Z";
        }
        public override void Create()
        {
            Children.Add(new SvgTitle { Content = Label });

            // 0,0 circle
            SvgCircle circle = Circles.CutCircle(FastenerDiameter, 0, 0);

            base.Children.Add(circle);
            var u = circle.CreateReference(0, Constants.JigHoleSpacing);
            
            Children.Add(u);
            
            var path = new SvgPath {
                Color = Colors.Cut()
            };
            
            path.PathData = SvgPathBuilder.Parse(ContourPath);
            path.Fill = SvgPaintServer.None;
            path.Stroke = Colors.Cut();
            path.StrokeWidth = Units.Mm(Constants.Kerf);
            path.ID = $"spacerOutline{idCounter++}";
            
            Children.Add(path);
        }
    }
}
