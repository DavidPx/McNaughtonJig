using Svg;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Text
    {
        internal static SvgText EtchedText(string text, float fontSize)
        {
            // TODO: instead return a few paths that look like letters
            return new SvgText(text)
            {
                FontSize = Units.Mm(fontSize),
                TextAnchor = SvgTextAnchor.Middle,
                Fill = SvgPaintServer.None,
                Stroke = Colors.Etch(),
                StrokeWidth = Units.Mm(Constants.Kerf)
            };
        }
    }
}
