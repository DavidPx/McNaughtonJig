using JigGenerator.Drawing.Primitives;
using Svg;
using Svg.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.Drawing.Parts
{
    public class CircleTown : Part
    {
        public CircleTown() : base(0)
        {
        }

        public override void Create()
        {
            // let's make circles!!

            var y = 0f;
            var g1 = new SvgGroup();

            for (float d = 5.9f; d <= 6.1f; d += 0.03f)
            {
                var c = Circles.CutCircle(d, 0, y);
                var t = Text.EtchedText($"{d:0.##}mm", 8f);
                t.Transforms.Add(new SvgTranslate(20f.Px(), (y + 4).Px()));

                g1.Children.Add(c);
                g1.Children.Add(t);
                y += 15f;
            }

            Children.Add(g1);
            var g2 = new SvgGroup();
            
            for (float d = 4.826f - 0.5f; d <= 4.826f + 0.5f; d += 0.06f)
            {
                var c = Circles.CutCircle(d, 0, y);
                var t = Text.EtchedText($"{d:0.##}mm", 8f);
                t.Transforms.Add(new SvgTranslate(20f.Px(), (y + 4).Px()));

                g2.Children.Add(c);
                g2.Children.Add(t);
                y += 15f;
            }

            Children.Add(g2);

        }
    }
}
