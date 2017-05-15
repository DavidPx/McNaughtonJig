using JigGenerator.Drawing.Primitives;
using Svg.Transforms;

namespace JigGenerator.Drawing.Parts
{
    public class TurretMount : Spacer
    {
        protected float postGap;

        protected const float postDiameter = 12.5f;
        
        public static TurretMount Large(float fastenerDiameter)
        {
            var path = "m 0,-26.00586 a 89.03535,196.4776 0 0 0 -74.627,89.70703 24.37795,24.37795 0 0 1 16.2188,22.93946 24.37795,24.37795 0 0 1 -24.377,24.37887 24.37795,24.37795 0 0 1 -1.8632,-0.0937 89.03535,196.4776 0 0 0 -1.6621,12.1015 24.37795,24.37795 0 0 1 7.5156,17.5665 24.37795,24.37795 0 0 1 -10.0781,19.705 89.03535,196.4776 0 0 0 -0.1622,10.1719 89.03535,196.4776 0 0 0 0.1387,8.7148 24.37795,24.37795 0 0 1 1.6426,-0.0585 24.37795,24.37795 0 0 1 24.3789,24.3769 24.37795,24.37795 0 0 1 -22.2031,24.2637 A 89.03535,196.4776 0 0 0 -5e-4,366.9492 89.03535,196.4776 0 0 0 89.0347,170.4707 89.03535,196.4776 0 0 0 -5e-4,-26.00586 Z";
            return new TurretMount(fastenerDiameter, "Large+", 5.98f, path);
        }

        public static TurretMount Standard(float fastenerDiameter)
        {
            var path = "M 0 -26.005859 A 89.03535 196.4776 0 0 0 -87.275391 132.51172 A 24.377953 24.377953 0 0 1 -78.560547 151.1875 A 24.377953 24.377953 0 0 1 -89.023438 171.16992 A 89.03535 196.4776 0 0 0 -88.849609 182.04492 A 24.377953 24.377953 0 0 1 -85.5 181.78516 A 24.377953 24.377953 0 0 1 -61.121094 206.16406 A 24.377953 24.377953 0 0 1 -84.664062 230.51172 A 89.03535 196.4776 0 0 0 0 366.94922 A 89.03535 196.4776 0 0 0 85.046875 227.96484 A 24.377953 24.377953 0 0 1 62.789062 203.69141 A 24.377953 24.377953 0 0 1 87.167969 179.31445 A 24.377953 24.377953 0 0 1 88.892578 179.40039 A 89.03535 196.4776 0 0 0 89.035156 170.4707 A 89.03535 196.4776 0 0 0 0 -26.005859 z ";
            return new TurretMount(fastenerDiameter, "Standard+", 5.98f, path);
        }

        public static TurretMount Small(float fastenerDiameter)
        {
            var path = "m 0,-26.005859 a 89.03535,196.4776 0 0 0 -89.0352,196.476559 89.03535,196.4776 0 0 0 0.4414,19.48242 24.377953,24.377953 0 0 1 8.8438,-1.66015 24.377953,24.377953 0 0 1 24.3789,24.37695 24.377953,24.377953 0 0 1 -24.3789,24.37891 24.377953,24.377953 0 0 1 -3.959,-0.35352 A 89.03535,196.4776 0 0 0 0,366.94922 89.03535,196.4776 0 0 0 84.959,228.55859 24.377953,24.377953 0 0 1 62.043,204.25391 a 24.377953,24.377953 0 0 1 24.3769,-24.37696 24.377953,24.377953 0 0 1 2.4629,0.125 89.03535,196.4776 0 0 0 0.1524,-9.53125 A 89.03535,196.4776 0 0 0 88.8418,158.38867 24.377953,24.377953 0 0 1 78.6562,138.59375 24.377953,24.377953 0 0 1 86.1113,121.05469 89.03535,196.4776 0 0 0 0,-26.005859 Z";
            return new TurretMount(fastenerDiameter, "Small+", 5.17f, path);
        }

        public static TurretMount Mini(float fastenerDiameter)
        {
            var path = "M 0 -26.005859 A 89.03535 196.4776 0 0 0 -89.035156 170.4707 A 89.03535 196.4776 0 0 0 0 366.94922 A 89.03535 196.4776 0 0 0 84.355469 232.78711 A 24.377953 24.377953 0 0 1 82.0625 232.91797 A 24.377953 24.377953 0 0 1 57.683594 208.54102 A 24.377953 24.377953 0 0 1 82.0625 184.16211 A 24.377953 24.377953 0 0 1 88.705078 185.10938 A 89.03535 196.4776 0 0 0 89.035156 170.4707 A 89.03535 196.4776 0 0 0 88.884766 163.82227 A 24.377953 24.377953 0 0 1 78.253906 143.69336 A 24.377953 24.377953 0 0 1 86.5625 125.40234 A 89.03535 196.4776 0 0 0 83.660156 104.17383 A 24.377953 24.377953 0 0 1 78.691406 104.71484 A 24.377953 24.377953 0 0 1 54.3125 80.335938 A 24.377953 24.377953 0 0 1 72.451172 56.785156 A 89.03535 196.4776 0 0 0 0 -26.005859 z ";
            return new TurretMount(fastenerDiameter, "Mini+", 3.26f, path);
        }
        
        internal TurretMount(float fastenerDiameter, string label, float postGap, string contourPath)
            : base(fastenerDiameter, label)
        {
            this.postGap = postGap;
            this.ContourPath = contourPath;
        }

        public override void Create()
        { 
            base.Create();
            
            var distanceBetweenCenters = postGap + postDiameter; // 2*r + gap

            var postHolesY = Constants.JigHoleSpacing - 25f;

            var leftPostX = -distanceBetweenCenters / 2f;
            var leftHole = Circles.CutCircle(postDiameter, leftPostX, postHolesY);
            var rightHole = leftHole.CreateReference(distanceBetweenCenters, 0);
            Children.Add(leftHole);
            Children.Add(rightHole);

            // Add a label;
            var label = Text.EtchedText(Label, 8f);
            label.Transforms.Add(new SvgTranslate(0, 45f.Px()));
            
            Children.Add(label);
        }
        
    }
}
