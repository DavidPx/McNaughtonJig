using Svg;
using JigGenerator.Drawing.Primitives;
using Svg.Transforms;
using System.Collections.Generic;

namespace JigGenerator.Drawing.Parts
{
    public class Spacer : SvgGroup, IPart
    {
        private static int idCounter = 0;

        private float fastenerDiameter;
        protected string Label;
        
        public Spacer(float fastenerDiameter, string label)
        {
            this.fastenerDiameter = fastenerDiameter;
            this.Label = label;
        }
        public virtual void Create()
        {
            Children.Add(new SvgTitle { Content = Label });

            // 0,0 circle
            SvgCircle circle = Circles.CutCircle(fastenerDiameter, 0, 0);

            base.Children.Add(circle);
            var u = circle.CreateReference(0, Constants.JigHoleSpacing);
            
            Children.Add(u);

            var path = new SvgPath {
                Color = Colors.Cut()
            };

            //var rawPath = "m 82.210613,311.32719 c 4.903234,0.248 9.851491,-0.42473 14.510548,-1.97276 6.845049,-2.27436 12.976639,-6.39768 18.224889,-11.34575 5.24825,-4.94806 9.65879,-10.7159 13.73385,-16.66748 5.27384,-7.70239 10.07878,-15.87608 12.62569,-24.85681 1.78373,-6.28964 2.42151,-12.83684 3.15642,-19.33308 1.14521,-10.12306 2.52865,-20.51171 0,-30.38054 -1.63435,-6.37852 -4.83943,-12.23906 -8.31522,-17.83151 -3.47578,-5.59246 -7.26224,-11.01064 -10.22875,-16.88912 -5.18715,-10.27894 -7.73022,-21.7076 -9.07471,-33.14243 -2.65911,-22.61561 -0.80292,-45.499389 -1.57821,-68.257584 -0.27634,-8.111907 -0.89432,-16.256253 -2.80974,-24.143621 -1.56085,-6.427312 -4.05214,-12.768427 -8.34345,-17.801442 C 98.710395,22.369935 90.534001,18.489928 82.210613,18.312064";
            var rawPath = "M 0.01346761,428.37881 C 7.5312157,428.75905 15.117974,427.7275 22.26134,425.35407 c 10.494956,-3.487 19.896051,-9.80893 27.942852,-17.39546 8.046647,-7.58653 14.808916,-16.42983 21.056943,-25.55495 8.085899,-11.80948 15.453023,-24.34156 19.357982,-38.11096 2.734806,-9.64349 3.712694,-19.68179 4.839457,-29.6419 1.755845,-15.52094 3.877055,-31.44896 0,-46.58001 -2.505742,-9.7798 -7.419867,-18.76523 -12.749035,-27.33977 C 77.380372,232.15664 71.574986,223.84934 67.026534,214.83631 59.073566,199.07649 55.174433,181.55377 53.113018,164.0217 49.036031,129.347 51.881995,94.261102 50.693289,59.367779 50.26966,46.930432 49.32213,34.443348 46.385399,22.35027 43.992196,12.495787 40.172483,2.7734672 33.592974,-4.9432622 25.311284,-14.656398 12.77506,-20.605294 0.01346761,-20.878007";
            path.PathData = SvgPathBuilder.Parse(rawPath);
            path.Fill = SvgPaintServer.None;
            path.Stroke = Colors.Cut();
            path.StrokeWidth = Units.Mm(Constants.Kerf);
            path.ID = $"spacerOutline{idCounter++}";

            // matrix(-1,0,0,1,0.02693522,0)
            var mirror = path.CreateReference();
            // To get this I mirrored, and then moved the clone to make its endpoints snap to the original's
            mirror.Transforms.Add(new SvgMatrix(new List<float> { -1f, 0f, 0f, 1f, 0f, 0.02693522f, 0f }));
            
            Children.Add(path);
            Children.Add(mirror);
        }
    }
}
