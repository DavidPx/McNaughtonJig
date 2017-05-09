using JigGenerator.Drawing;
using JigGenerator.Drawing.Parts;
using Svg;
using Svg.Transforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator
{
    class Program
    {
        private const int boltDiameter = 6;

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
            

            var bigTurret = TurretMount.JumboAndStandard(boltDiameter);
            bigTurret.Create();
            bigTurret.Transforms.Add(new SvgTranslate(p1Size * 2 / 3, p1Size * 2 / 3));
            doc.Children.Add(bigTurret);

            var protractor = new Protractor(boltDiameter, 12, 10);
            protractor.Create();
            doc.Children.Add(protractor);
            
            File.WriteAllText(@"c:\temp\Drawing.svg", doc.GetXML());
        }
    }
}
