using Svg;

namespace JigGenerator.Drawing.Primitives
{
    public static class Units
    {
        public static SvgUnit Mm(float valueInMm)
        {
            return new SvgUnit(SvgUnitType.Millimeter, valueInMm);
        }

        /// <summary>
        /// Converts a millimeter float value to one in px
        /// </summary>
        /// <param name="millimeters"></param>
        /// <returns></returns>
        public static float Px(this float millimeters)
        {

            // In Inkscape a 50mm shift resulted in a 227.95276 value in translate.  But this was apparently wrong??
            // also, 18mm => 68.031494.  This results in spot-on mm values in Inkscape.

            //return millimeters * (227.95276f / 50f);
            return millimeters * (68.031494f / 18f);
        }
    }
}
