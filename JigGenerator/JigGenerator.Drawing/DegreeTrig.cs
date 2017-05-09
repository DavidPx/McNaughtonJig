using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JigGenerator.Drawing
{
    internal static class DegreeTrig
    {
        internal static float Sin(float angle)
        {
            return (float)Math.Sin(angle * (float)Math.PI / 180f);
        }

        internal static float Cos(float angle)
        {
            return (float)Math.Cos(angle * (float)Math.PI / 180f);
        }
    }
}
