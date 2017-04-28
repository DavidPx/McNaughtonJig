using Svg;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Units
    {
        internal static SvgUnit Mm(float valueInMm)
        {
            return new SvgUnit(SvgUnitType.Millimeter, valueInMm);
        }
    }
}
