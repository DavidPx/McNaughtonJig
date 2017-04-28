using Svg;
using System.Drawing;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Colors
    {
        internal static SvgColourServer Cut()
        {
            return new SvgColourServer(Color.Blue);
        }

        internal static SvgColourServer Etch()
        {
            return new SvgColourServer(Color.Red);
        }
    }
}
