using System;
using Svg;
using JigGenerator.Drawing.Primitives;

namespace JigGenerator.Drawing
{
    public class Spacer : SvgGroup, IPart
    {
        private float fastenerDiameter;
        
        public Spacer(float fastenerDiameter)
        {
            this.fastenerDiameter = fastenerDiameter;
        }
        public virtual void Create()
        {
            Children.Add(Circles.CutCircle(fastenerDiameter, 0, 0));
            Children.Add(Circles.CutCircle(fastenerDiameter, 0, 90));

            // TODO: add fancy outline
        }
    }
}
