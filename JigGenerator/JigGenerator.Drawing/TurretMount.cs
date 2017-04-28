using System;

namespace JigGenerator.Drawing
{
    public class TurretMount : Spacer, IPart
    {
        private float postDistance;
        private const float postDiameter = 12f;
        
        public static TurretMount JumboAndStandard(float fastenerDiameter)
        {
            return new TurretMount(fastenerDiameter) { postDistance = 30f };
        }

        public static TurretMount Mini(float fastenerDiameter)
        {
            return new TurretMount(fastenerDiameter) { postDistance = 29.2f };
        }

        // what is the last 27.3mm gap for???

        private TurretMount(float fastenerDiameter)
            : base(fastenerDiameter)
        {

        }

        public override void Create()
        {
            base.Create();

            // add the 12mm post holes
        }
    }
}
