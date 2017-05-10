using Svg;

namespace JigGenerator.Drawing.Parts
{
    public abstract class Part : SvgGroup
    {
        protected float FastenerDiameter;

        protected Part(float fastenerDiameter)
        {
            FastenerDiameter = fastenerDiameter;
        }
    }
}
