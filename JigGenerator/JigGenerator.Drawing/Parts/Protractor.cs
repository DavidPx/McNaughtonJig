using JigGenerator.Drawing.Primitives;
using Svg;
using Svg.Pathing;
using Svg.Transforms;
using System;
using System.Drawing;

namespace JigGenerator.Drawing.Parts
{
    public class Protractor : SvgGroup, IPart
    {
        private float armLength;
        private const float baseWidth = 109f * 2;
        private const float armThickness = 25.4f;
        private  float bodyRadius;
        private const int totalProtractorAngleSweep = 120;
        private const int startAngle = 90 - totalProtractorAngleSweep / 2;
        private const float majorDivisionLength = 7f;
        private const float minorDivisionLength = 4f;
        private const float textHeight = 4f;

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

            // put the text a comfortable distance away from the slot
            textBaselineRadius = outerArcRadius + 2f;

            // put the major lines a comfortable distance away from the top of the text
            bodyRadius = textBaselineRadius + textHeight + 1f + majorDivisionLength;
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
            // Baseline is the bottom hole because most shapes are arcs with it as their center
            Children.Add(Circles.CutCircle(fastenerDiameter, 0, 0));
            Children.Add(CreateAngleMarks());
            Children.Add(CreateSlotPath());
            Children.Add(CreateAngleText());
            Children.Add(CreateOutline());
        }

        private SvgVisualElement CreateOutline()
        {
            const float cornerRadius = 3f;

            var path = Paths.CutPath();

            var bottomY = armThickness / 2;
            var rightBorderX = bodyRadius;

            // start drawing the path @ E.  E, F, G, A, B, C, D
            var pointE = Points.MetricPoint(rightBorderX, 0);
            var pointF = Points.MetricPoint(rightBorderX, bottomY - cornerRadius);
            var pointG = Points.MetricPoint(rightBorderX - cornerRadius, bottomY);

            path.PathData.Add(new SvgMoveToSegment(pointE));
            path.PathData.Add(new SvgLineSegment(pointE, pointF));
            path.PathData.Add(Arcs.SimpleSegment(pointF, cornerRadius, SvgArcSweep.Positive, pointG));

            if (armLength > 0)
            {
                float leftArmX = -bodyRadius - armLength + cornerRadius;
                var pointA = Points.MetricPoint(leftArmX, bottomY);
                var pointB = Points.MetricPoint(leftArmX, bottomY - armThickness);
                
                var dY = bottomY - armThickness - cornerRadius;
                var dX = -(float)Math.Sqrt(Math.Pow(bodyRadius, 2) - Math.Pow(dY, 2));
                var pointC = Points.MetricPoint(dX - cornerRadius, dY + cornerRadius);
                var pointD = Points.MetricPoint(dX, dY);

                path.PathData.Add(new SvgLineSegment(pointG, pointA));
                path.PathData.Add(Arcs.SimpleSegment(pointA, armThickness / 2, SvgArcSweep.Positive, pointB));
                path.PathData.Add(new SvgLineSegment(pointB, pointC));
                path.PathData.Add(Arcs.SimpleSegment(pointC, cornerRadius, SvgArcSweep.Negative, pointD));
                path.PathData.Add(Arcs.SimpleSegment(pointD, bodyRadius, SvgArcSweep.Positive, pointE));
            }
            // todo: else for zero arm

            

            

            return path;
        }

        private SvgGroup CreateAngleText()
        {
            var group = new SvgGroup();

            var majorAngleIncrement = (float)totalProtractorAngleSweep / majorDivisions;
            
            for (int i = 0; i <= majorDivisions; i++)
            {
                // The angle from the horizontal, used for positioning each label
                var a = startAngle + i * majorAngleIncrement;

                // this is the angle from the vertical.  The graduation mark function uses the angle from the horizontal. 
                // the letters start rotated left -60
                var textRotation = -90 + a;

                // go from -n to n
                var number = -totalProtractorAngleSweep / 2 + i * majorAngleIncrement;

                var label = Text.EtchedText(number.ToString("#0"), textHeight);
                float x = -textBaselineRadius * DegreeTrig.Cos(a);
                float y = -textBaselineRadius * DegreeTrig.Sin(a);
                label.Transforms.Add(new SvgTranslate(x.Px(), y.Px()));
                label.Transforms.Add(new SvgRotate(textRotation));
                
                group.Children.Add(label);
            }

            return group;
        }

        private SvgGroup CreateAngleMarks()
        {
            var group = new SvgGroup();
            var minorIncrement = (float)totalProtractorAngleSweep / (minorDivisions * majorDivisions);
            var doMidPoint = minorDivisions % 2 == 0;
            
            // Spin through the minor divisions selecting the length of the line as we go
            for (int i = 0; i <= majorDivisions * minorDivisions; ++i)
            {
                var a = startAngle + i * minorIncrement;
                float lineLength;

                if (i % minorDivisions == 0) // 0, 10, 20 should draw major lines
                    lineLength = majorDivisionLength; // major 
                else if (doMidPoint && i % (minorDivisions / 2) == 0)
                    lineLength = minorDivisionLength + 1.5f; // midpoint 
                else
                    lineLength = minorDivisionLength;
                
                var l = Lines.EtchLine(
                    // all lines start on the edge and continue along their length
                    -bodyRadius * DegreeTrig.Cos(a),
                    -bodyRadius * DegreeTrig.Sin(a),
                    -(bodyRadius - lineLength) * DegreeTrig.Cos(a),
                    -(bodyRadius - lineLength) * DegreeTrig.Sin(a));

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

            var pointA = Points.MetricPoint(-outerArcX, outerArcY);
            var pointB = Points.MetricPoint(outerArcX, outerArcY);
            var pointC = Points.MetricPoint(innerArcX, innerArcY);
            var pointD = Points.MetricPoint(-innerArcX, innerArcY);

            var slotPath = Paths.CutPath();

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
                            new PointF(startX.Px(), startY.Px()),
                            fastenerDiameter / 2f,
                            fastenerDiameter / 2f,
                            0f,// 180 * (float)radiansPerDegree,
                            SvgArcSize.Small,
                            SvgArcSweep.Positive,
                            new PointF(endX.Px(), endY.Px())
                            );
        }

        private static SvgArcSegment MakeSlotArc(float radius, float x, float y)
        {
            var arc = new SvgArcSegment(
                new PointF(-x, y),
                radius.Px(),
                radius.Px(),
                0f,
                SvgArcSize.Small,
                SvgArcSweep.Positive,
                new PointF(x, y));
            
            return arc;
        }

        //private static SvgArcSegment MakeEndCap(float radius, )
    }
}
