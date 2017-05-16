using Svg;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Lines
    {
        /// <summary>
        /// Etches a line with the values being in millimeters
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        internal static SvgLine EtchLine(float startX, float startY, float endX, float endY)
        {
            return Line(startX, startY, endX, endY, Colors.Etch());
        }

        /// <summary>
        /// Cuts a line with the values being in millimeters
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        internal static SvgLine CutLine(float startX, float startY, float endX, float endY)
        {
            return Line(startX, startY, endX, endY, Colors.Cut());
        }

        private static SvgLine Line(float startX, float startY, float endX, float endY, SvgColourServer color)
        {
            return new SvgLine
            {
                StartX = startX.Px(),
                StartY = startY.Px(),
                EndX = endX.Px(),
                EndY = endY.Px(),
                Stroke = color,
                StrokeWidth = Units.Mm(Constants.Kerf),
                Fill = SvgPaintServer.None,
            };
        }

        
    }
}
