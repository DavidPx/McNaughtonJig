using Svg;
using Svg.Transforms;
using System;

namespace JigGenerator.Drawing.Primitives
{
    internal static class Element
    {
        /// <summary>
        /// Makes an <see cref="SvgUse"/> reference to the given element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        internal static SvgUse CreateReference(this SvgElement element)
        {
            if (string.IsNullOrWhiteSpace(element.ID))
                throw new ArgumentException("element's ID not set");

            return new SvgUse
            {
                ReferencedElement = new Uri($"#{element.ID}", UriKind.Relative)
            };
        }

        /// <summary>
        /// Makes an <see cref="SvgUse"/> reference to the given element along with a translation (move) transform in millimeters
        /// </summary>
        /// <param name="element"></param>
        /// <param name="transformXMm"></param>
        /// <param name="transformYMm"></param>
        /// <returns></returns>
        internal static SvgUse CreateReference(this SvgElement element, float transformXMm, float transformYMm)
        {
            var e = element.CreateReference();
            e.Transforms.Add(new SvgTranslate(transformXMm.Px(), transformYMm.Px()));
            return e;
        }
    }
}
