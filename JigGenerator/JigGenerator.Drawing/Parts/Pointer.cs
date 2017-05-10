using JigGenerator.Drawing.Primitives;
using Svg;
using Svg.Transforms;
using System;
using System.Linq;

namespace JigGenerator.Drawing.Parts
{
    public class Pointer : Part
    {
        float[] radii;

        /// <summary>
        /// Makes a pointer with default radii
        /// </summary>
        /// <param name="fastenerDiameter"></param>
        public Pointer(float fastenerDiameter) 
            : base(fastenerDiameter)
        {
            radii = new float[] { 89, 137, 176, 275 };
        }

        /// <summary>
        /// Makes a pointer with custom radii
        /// </summary>
        /// <param name="fastenerDiameter"></param>
        /// <param name="radii"></param>
        public Pointer(float fastenerDiameter, float[] radii)
            : this(fastenerDiameter)
        {
            if (radii.Length < 1)
                throw new ArgumentException(nameof(radii), "Enter more than one cutter radius");
        }
                
        public override void Create()
        {
            // top hole is the origin point
            var topHole = Circles.CutCircle(FastenerDiameter, 0, 0);
            var bottomHole = topHole.CreateReference(0, Constants.JigHoleSpacing);

            var smallestRadius = radii.Min();
            const float originToSmallestLength = 50f;
            var angleAlpha = Math.Asin(originToSmallestLength / (2 * smallestRadius)) * 2; // radians
            var angleBeta = Math.PI / 2 - angleAlpha;
            var h = smallestRadius / Math.Cos(angleAlpha);
            var x = h - smallestRadius;
            var pivotY = x * Math.Tan(angleBeta);

            foreach (var radius in radii)
            {
                var d = Math.Sqrt(Math.Pow(pivotY, 2) + Math.Pow(radius, 2));
                var angleC = Math.Asin(radius / d);
                var angleR = Math.PI - 2 * angleC;

                // Finally we can make a stupid circle.  Start it at 0,0 and rotate it R around the pivot-Y point.
                var hole = Circles.CutCircle(FastenerDiameter, 0, 0);
                hole.Transforms.Add(new SvgRotate(DegreeTrig.Degrees(-angleR), 0, (Constants.JigHoleSpacing - (float)pivotY).Px()));

                Children.Add(hole);
            }
            Children.Add(topHole);
            Children.Add(bottomHole);

            // TODO: add etched line for seeing the angle, outline
        }
    }
}
