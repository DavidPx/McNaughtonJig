using System.Drawing;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Points
    {
        /// <summary>
        /// Makes a <see cref="PointF"/> with values in millimeters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static PointF MetricPoint(float x, float y)
        {
            return new PointF(x.Px(), y.Px());
        }
    }
}
