using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Text
    {
        internal static SvgText EtchedText(string text)
        {
            var label = new SvgText(text)
            {
                FontSize = Units.Mm(10f),
                TextAnchor = SvgTextAnchor.Middle
            };
            label.Fill = SvgPaintServer.None;
            label.Stroke = Colors.Etch();
            label.StrokeWidth = Units.Mm(Constants.Kerf);


            return label;
        }
    }
}
