using JigGenerator.Drawing.Primitives;
using Svg;
using Svg.Transforms;
using System;
using System.Diagnostics;
using System.Linq;

namespace JigGenerator.Drawing.Parts
{
    public class Pointer : Part
    {
        private const float widthAboveRadiusHoles = 20f;
        private const float guideLineLength = 10f;
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
            const float jigTopRadius = Constants.JigHoleSpacing + widthAboveRadiusHoles;

            CreateRadiusHoles();

            Children.Add(Lines.EtchLine(0, widthAboveRadiusHoles, 0, widthAboveRadiusHoles - guideLineLength));

            // TODO: add etched line for seeing the angle, outline
        }

        private void CreateRadiusHoles()
        {
            // top hole is the origin point
            // pivot point Y is the bottom  hole
            // The tip of the jig is pivotY beyond the bottom hole

            var topHole = Circles.CutCircle(FastenerDiameter, 0, 0);

            var smallestRadius = radii.Min();
            const double originToSmallestLength = 50;
            var angleAlpha = Math.Asin(originToSmallestLength / (2 * smallestRadius)) * 2; // radians
            var angleBeta = Math.PI / 2 - angleAlpha;
            var h = smallestRadius / Math.Cos(angleAlpha);
            var x = h - smallestRadius;
            var pivotY = x * Math.Tan(angleBeta);

            var bottomHole = topHole.CreateReference(0, Constants.JigHoleSpacing);

            foreach (var radius in radii)
            {
                var d = Math.Sqrt(Math.Pow(pivotY, 2) + Math.Pow(radius, 2));
                var angleC = Math.Asin(radius / d);
                var angleR = Math.PI - 2 * angleC;

                Debug.WriteLine($"r: {DegreeTrig.Degrees(angleR)}");

                // Finally we can make a stupid circle.  Start it at 0,0 and rotate it R around the pivot-Y point.
                var hole = Circles.CutCircle(FastenerDiameter, 0, 0);
                hole.Transforms.Add(new SvgRotate(DegreeTrig.Degrees(-angleR), 0, Constants.JigHoleSpacing.Px()));

                Children.Add(hole);
            }
            Children.Add(topHole);
            Children.Add(bottomHole);
        }
    }
}
