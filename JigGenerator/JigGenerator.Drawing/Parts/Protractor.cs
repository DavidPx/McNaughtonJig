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
        private const float bodyRadius = 110f;
        private const int totalProtractorAngleSweep = 120;
        private const int startAngle = 90 - totalProtractorAngleSweep / 2;
        private const float majorDivisionLength = 7f;
        private const float minorDivisionLength = 4f;

        private float fastenerDiameter;
        private ushort majorDivisions;
        private ushort minorDivisions;

        private float outerArcRadius;
        private float textBaselineRadius;
        
        public Protractor(float fastenerDiameter, ushort majorDivisions, ushort minorDivisions)
        {
            armLength = 0;
            this.fastenerDiameter = fastenerDiameter;
            
            this.majorDivisions = majorDivisions;
            this.minorDivisions = minorDivisions;

            outerArcRadius = Constants.JigHoleSpacing + (fastenerDiameter / 2f - Constants.Kerf);
        }
        public Protractor(float fastenerDiameter, float armLength, ushort majorDivisions, ushort minorDivisions)
            : this(fastenerDiameter, majorDivisions, minorDivisions)
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
            var slotPath = CreateSlotPath();

            Children.Add(CreateAngleMarks());
            Children.Add(hole);
            Children.Add(slotPath);
        }

        private SvgGroup CreateAngleMarks()
        {
            var group = new SvgGroup();
            var majorIncrement = (float)totalProtractorAngleSweep / majorDivisions;
            var minorIncrement = (float)totalProtractorAngleSweep / (minorDivisions * majorDivisions);

            // Major divisions
            for (float a = startAngle; a <= totalProtractorAngleSweep + startAngle; a += majorIncrement)
            {
                var l = Lines.EtchLine(
                    bodyRadius * DegreeTrig.Cos(a),
                    -bodyRadius * DegreeTrig.Sin(a),
                    (bodyRadius - majorDivisionLength) * DegreeTrig.Cos(a),
                    -(bodyRadius - majorDivisionLength) * DegreeTrig.Sin(a));
                group.Children.Add(l);
            }

            // Minor divisions
            for (int i = 0; i < majorDivisions * minorDivisions; ++i)
            {
                
                // float a = startAngle; a <= totalProtractorAngleSweep + startAngle; a += minorIncrement
                // skip majors
                if (i % minorDivisions == 0)
                    continue;

                var a = startAngle + i * minorIncrement;

                var midpointExtra = 0f;

                if (minorDivisions % 2 == 0 && i % (minorDivisions / 2) == 0)
                    midpointExtra = 1.5f;

                var l = Lines.EtchLine(
                    bodyRadius * DegreeTrig.Cos(a),
                    -bodyRadius * DegreeTrig.Sin(a),
                    (bodyRadius - minorDivisionLength - midpointExtra) * DegreeTrig.Cos(a),
                    -(bodyRadius - minorDivisionLength - midpointExtra) * DegreeTrig.Sin(a));
                group.Children.Add(l);
            }

            return group;
        }

        private SvgPath CreateSlotPath()
        {
            var endCapRadius = fastenerDiameter / 2f - Constants.Kerf;
            var innerArcRadius = Constants.JigHoleSpacing - (fastenerDiameter / 2f - Constants.Kerf);
            
            float innerArcX = innerArcRadius * DegreeTrig.Cos(startAngle);
            float innerArcY = -innerArcRadius * DegreeTrig.Sin(startAngle); // y's are negative b/c they are above the origin hole
            float outerArcX = outerArcRadius * DegreeTrig.Cos(startAngle);
            float outerArcY = -outerArcRadius * DegreeTrig.Sin(startAngle);

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
            return slotPath;
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
