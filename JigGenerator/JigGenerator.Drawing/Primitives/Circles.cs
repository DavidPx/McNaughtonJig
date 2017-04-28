using Svg;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Circles
    {
        internal static SvgCircle CutCircle(float diameter, float centerX, float centerY)
        {
            return new SvgCircle
            {
                Radius = Units.Mm(diameter / 2 - Constants.Kerf / 2),
                Stroke = Colors.Cut(),
                StrokeWidth = Units.Mm(Constants.Kerf),
                Fill = SvgPaintServer.None,
                CenterX = Units.Mm(centerX),
                CenterY = Units.Mm(centerY)
            };
        }
    }
}
