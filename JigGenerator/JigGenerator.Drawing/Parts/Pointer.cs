using Svg;
using System;

namespace JigGenerator.Drawing.Parts
{
    public class Pointer : Part
    {
        int[] radii;

        public Pointer(float fastenerDiameter) : base(fastenerDiameter)
        {
            Create();
        }

        public Pointer(float fastenerDiameter, int[] radii)
            : this(fastenerDiameter)
        {
            if (radii.Length < 1)
                throw new ArgumentException(nameof(radii), "Enter more than one cutter radius");
        }

        
        public override void Create()
        {
            
        }
    }
}
