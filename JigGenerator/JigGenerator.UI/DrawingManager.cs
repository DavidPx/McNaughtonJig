using JigGenerator.Drawing.Parts;
using JigGenerator.UI.Options;
using Svg;
using Svg.Transforms;

namespace JigGenerator.UI
{
    internal class DrawingManager
    {
        internal SvgDocument CreateDocument(JigOptions options)
        {
            var width = new SvgUnit(SvgUnitType.Millimeter, options.Drawing.Width);
            var height = new SvgUnit(SvgUnitType.Millimeter, options.Drawing.Height);

            var doc = new SvgDocument
            {
                Width = width,
                Height = height
            };

            var spacer = new Spacer(options.FastenerDiameter, "Spacer");
            spacer.Create();
            spacer.Transforms.Add(new SvgTranslate(width / 3, width / 3));
            doc.Children.Add(spacer);


            if (options.TurretMount.MakeJumoAndStandard)
            {
                var bigTurret = TurretMount.Large(options.FastenerDiameter);
                bigTurret.Create();
                bigTurret.Transforms.Add(new SvgTranslate(width * 2 / 3, width * 2 / 3));
                doc.Children.Add(bigTurret);
            }

            if (options.TurretMount.MakeMini)
            {
                var mini = TurretMount.Small(options.FastenerDiameter);
                mini.Create();
                mini.Transforms.Add(new SvgTranslate(width, height));
                doc.Children.Add(mini);
            }
            

            var protractor = new Protractor(options.FastenerDiameter, options.Protractor.ArmLength, options.Protractor.MajorDivisions, options.Protractor.MinorDivisions);
            protractor.Create();
            protractor.Transforms.Add(new SvgTranslate(width, 0));
            doc.Children.Add(protractor);

            var pointer = new Pointer(options.FastenerDiameter, options.Pointer.CutterRadii);
            pointer.Create();
            doc.Children.Add(pointer);

            return doc;
        }
    }
}
