using JigGenerator.Drawing.Primitives;
using Svg.Transforms;

namespace JigGenerator.Drawing.Parts
{
    public class TurretMount : Spacer, IPart
    {
        /* Post distance is the measured space between the outer edges of the adjoining posts.
         *  
         *   ---     ---
         *   | |     | |
         *   ---     ---
         *   
         *   |   here  |
         *   
         */
        private float postDistance;

        private const float postDiameter = 12f;
        
        public static TurretMount JumboAndStandard(float fastenerDiameter)
        {
            return new TurretMount(fastenerDiameter, "JS") { postDistance = 30f};
        }

        public static TurretMount Mini(float fastenerDiameter)
        {
            return new TurretMount(fastenerDiameter, "Mini") { postDistance = 29.2f };
        }

        // what is the last 27.3mm gap for???

        private TurretMount(float fastenerDiameter, string label)
            : base(fastenerDiameter, label)
        {

        }

        public override void Create()
        {
            base.Create();

            // add the 12mm post holes
            var distanceBetweenCenters = postDistance - postDiameter;

            var postHolesY = Constants.JigHoleSpacing - 25f;

            var leftHole = Circles.CutCircle(postDiameter, -distanceBetweenCenters / 2f, postHolesY);
            var rightHole = leftHole.CreateReference(distanceBetweenCenters, 0);
            Children.Add(leftHole);
            Children.Add(rightHole);

            // Add a label;
            var label = Text.EtchedText(Label, 10f);
            label.Transforms.Add(new SvgTranslate(0, 45f.Px()));

            Children.Add(label);

        }
        
    }
}
