using JigGenerator.Drawing.Primitives;
using Svg;
using Svg.Pathing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.Drawing.Parts
{
    public class Protractor : SvgGroup, IPart
    {
        private float armLength;
        private const float baseWidth = 109f * 2;
        private const float armThickness = 25.4f;
        private float fastenerDiameter;
        private const double radiansPerDegree = Math.PI / 180.0;

        public Protractor(float fastenerDiameter)
        {
            armLength = 0;
            this.fastenerDiameter = fastenerDiameter;
        }
        public Protractor(float fastenerDiameter, float armLength)
            : this(fastenerDiameter)
        {
            if (armLength > 0 && armLength < 10)
                throw new ArgumentOutOfRangeException(nameof(armLength), "If you are specifying an arm length please make it longer than 10mm");
            if (armLength < 0)
                throw new ArgumentOutOfRangeException(nameof(armLength), "Arm length cannnot be negative");

            this.armLength = armLength;
        }

        public void Create()
        {
            // Baseline is the bottom hole
            var hole = Circles.CutCircle(fastenerDiameter, 0, 0);

            var endCapRadius = fastenerDiameter / 2f;
            var innerArcRadius = Constants.JigHoleSpacing - (fastenerDiameter / 2f);
            var outerArcRadius = Constants.JigHoleSpacing + (fastenerDiameter / 2f);

            float innerArcX = innerArcRadius * (float)Math.Cos(30 * radiansPerDegree);
            float innerArcY = -innerArcRadius * (float)Math.Sin(30 * radiansPerDegree); // y's are negative b/c they are above the origin hole
            float outerArcX = outerArcRadius * (float)Math.Cos(30 * radiansPerDegree);
            float outerArcY = -outerArcRadius * (float)Math.Sin(30 * radiansPerDegree);

            var pointA = new PointF(-outerArcX.Px(), outerArcY.Px());
            var pointB = new PointF(outerArcX.Px(), outerArcY.Px());
            var pointC = new PointF(innerArcX.Px(), innerArcY.Px());
            var pointD = new PointF(-innerArcX.Px(), innerArcY.Px());
            
            var slotPath = new SvgPath
            {
                Fill = SvgPaintServer.None,
                Stroke = Colors.Cut(),
                StrokeWidth = Units.Mm(Constants.Kerf)
            };

            var leftEndCap = MakeEndCap(-innerArcX, innerArcY, -outerArcX, outerArcY);
            var rightEndCap = MakeEndCap(outerArcX, outerArcY, innerArcX, innerArcY);
            
            slotPath.PathData.Add(new SvgMoveToSegment(pointA));
            slotPath.PathData.Add(Arcs.SimpleSegment(pointA, outerArcRadius, SvgArcSweep.Positive, pointB));
            slotPath.PathData.Add(Arcs.SimpleSegment(pointB, endCapRadius, SvgArcSweep.Positive, pointC));
            slotPath.PathData.Add(Arcs.SimpleSegment(pointC, innerArcRadius, SvgArcSweep.Negative, pointD)); // Negative makes the lower arc path back "into" the body of the shape
            slotPath.PathData.Add(Arcs.SimpleSegment(pointD, endCapRadius, SvgArcSweep.Positive, pointA));
            
            Children.Add(hole);
            Children.Add(slotPath);
        }

        private SvgArcSegment MakeEndCap(float startX, float startY, float endX, float endY)
        {
            return new SvgArcSegment(
                            new System.Drawing.PointF(startX.Px(), startY.Px()),
                            fastenerDiameter / 2f,
                            fastenerDiameter / 2f,
                            0f,// 180 * (float)radiansPerDegree,
                            SvgArcSize.Small,
                            SvgArcSweep.Positive,
                            new System.Drawing.PointF(endX.Px(), endY.Px())
                            );
        }

        private static SvgArcSegment MakeSlotArc(float radius, float x, float y)
        {
            var arc = new SvgArcSegment(
                new System.Drawing.PointF(-x, y),
                radius.Px(),
                radius.Px(),
                0f,// 120f,
                SvgArcSize.Small,
                SvgArcSweep.Positive,
                new System.Drawing.PointF(x, y));
            
            return arc;
        }

        //private static SvgArcSegment MakeEndCap(float radius, )
    }
}
