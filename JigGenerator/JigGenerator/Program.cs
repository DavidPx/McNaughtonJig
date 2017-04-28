using JigGenerator.Drawing;
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
        static void Main(string[] args)
        {
            var p1Size = new SvgUnit(SvgUnitType.Millimeter, 181);
            var doc = new SvgDocument
            {
                Width = p1Size,
                Height = p1Size
            };

            var spacer = new Spacer(6);

            spacer.Create();

            spacer.Transforms.Add(new SvgTranslate(p1Size / 2, p1Size / 2));

            doc.Children.Add(spacer);

            File.WriteAllText(@"c:\temp\Drawing.svg", doc.GetXML());
        }
    }
}
