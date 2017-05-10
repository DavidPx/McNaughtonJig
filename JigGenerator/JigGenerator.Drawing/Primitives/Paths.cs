using Svg;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Paths
    {
        internal static SvgPath CutPath()
        {
            return new SvgPath
            {
                Fill = SvgPaintServer.None,
                Stroke = Colors.Cut(),
                StrokeWidth = Units.Mm(Constants.Kerf)
            };
        }
    }
}
