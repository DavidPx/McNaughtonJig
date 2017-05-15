using JigGenerator.Drawing.Parts;
using JigGenerator.Drawing.Primitives;
using Svg;
using Svg.Transforms;
using System.IO;

namespace JigGenerator
{
    class Program
    {
        private const float boltDiameter = 4.9f; // #10 is 0.19", or 4.826mm

        static void Main(string[] args)
        {
            var p1Size = new SvgUnit(SvgUnitType.Millimeter, 181);
            var doc = new SvgDocument
            {
                Width = p1Size,
                Height = p1Size
            };

            var spacer = new Spacer(boltDiameter, "Spacer");
            spacer.Create();
            spacer.Transforms.Add(new SvgTranslate(p1Size / 3, p1Size / 3));
            doc.Children.Add(spacer);

            var circles = new CircleTown();
            circles.Create();
            doc.Children.Add(circles);

            var turretX = p1Size * 2 / 3;
            float turretY = p1Size * 2 / 3;
            var bigTurret = TurretMount.Large(boltDiameter);
            bigTurret.Create();
            
            bigTurret.Transforms.Add(new SvgTranslate(turretX, 0));
            doc.Children.Add(bigTurret);

            var standardTurret = TurretMount.Standard(boltDiameter);
            standardTurret.Create();
            standardTurret.Transforms.Add(new SvgTranslate(turretX + 150f.Px(), 0));
            doc.Children.Add(standardTurret);

            var smallTurret = TurretMount.Small(boltDiameter);
            smallTurret.Create();
            smallTurret.Transforms.Add(new SvgTranslate(turretX + 300f.Px(), 0));
            doc.Children.Add(smallTurret);

            var miniTurret = TurretMount.Mini(boltDiameter);
            miniTurret.Create();
            miniTurret.Transforms.Add(new SvgTranslate(turretX + 450f.Px(), 0));
            doc.Children.Add(miniTurret);

            var protractor = new Protractor(boltDiameter, 20, 12, 10);
            protractor.Create();
            protractor.Transforms.Add(new SvgTranslate(p1Size, 0));
            doc.Children.Add(protractor);

            var pointer = new Pointer(boltDiameter, new float[] { 83, 112, 231, 170 });
            pointer.Create();
            doc.Children.Add(pointer);
            
            File.WriteAllText(@"c:\temp\Drawing.svg", doc.GetXML());
        }
    }
}
