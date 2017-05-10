using Svg.Pathing;
using System.Drawing;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Arcs
    {
        // slotPath.PathData.Add(new SvgArcSegment(pointA, outerArcRadius, outerArcRadius, 0f, SvgArcSize.Small, SvgArcSweep.Positive, pointB)); // outer arc
        internal static SvgArcSegment SimpleSegment(PointF start, float radius, SvgArcSweep sweep, PointF end)
        {
            //start.X = start.X.Px();
            //start.Y = start.Y.Px();

            //end.X = end.X.Px();
            //end.Y = end.Y.Px();

            return new SvgArcSegment(start, radius.Px(), radius.Px(), 0f, SvgArcSize.Small, sweep, end);
        }
    }
}
