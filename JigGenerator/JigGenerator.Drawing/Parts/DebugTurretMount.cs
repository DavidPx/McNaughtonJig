using JigGenerator.Drawing.Primitives;
using Svg.Transforms;

namespace JigGenerator.Drawing.Parts
{
    /// <summary>
    /// This lets you draw the full turret plus the mounting piece so that you can more easily draw a nice-looking mount that also clears the surrounding posts.
    /// </summary>
    public class DebugTurretMount : TurretMount
    {
        class Post
        {
            internal float GapToRight;
            internal float AngleToRight;
            internal float AngleFromRight;
        }

        private short referencePair;

        private static Post[] allPosts = new[] 
        {
            new Post { GapToRight = 3.26f, AngleToRight = 0, AngleFromRight = 180f - 145.1f },
            new Post { GapToRight = 5.17f, AngleToRight = 180f - 145.1f, AngleFromRight = 180f - 142.5f },
            new Post { GapToRight = 6f, AngleToRight = 180f - 142.5f, AngleFromRight = 180f - 141.7f },
            new Post { GapToRight = 5.92f, AngleToRight = 180f - 141.7f },
            new Post { GapToRight = 0f, AngleToRight = 0, AngleFromRight = 0 }, // last one gets off easy!
        };

        private const string ellipse = "M 89.035347,170.47115 A 89.035347,196.47765 0 0 1 0,366.94879 89.035347,196.47765 0 0 1 -89.035347,170.47115 89.035347,196.47765 0 0 1 0,-26.0065 89.035347,196.47765 0 0 1 89.035347,170.47115 Z";

        public new static DebugTurretMount Large(float fastenerDiameter)
        {
            return new DebugTurretMount(fastenerDiameter, "Large", 5.92f, ellipse, 3);
        }

        public new static DebugTurretMount Standard(float fastenerDiameter)
        {
            return new DebugTurretMount(fastenerDiameter, "Stadard", 6f, ellipse, 2);
        }

        public new static DebugTurretMount Small(float fastenerDiameter)
        {
            return new DebugTurretMount(fastenerDiameter, "Small", 5.17f, ellipse, 1);
        }

        public new static DebugTurretMount Mini(float fastenerDiameter)
        {
            return new DebugTurretMount(fastenerDiameter, "Mini", 3.26f, ellipse, 0);
        }
        
        private DebugTurretMount(float fastenerDiameter, string label, float postGap, string contourPath, short referencePair)
            : base(fastenerDiameter, label, postGap, contourPath)
        {
            this.referencePair = referencePair;
        }

        public override void Create()
        { 
            base.Create();
            
            var distanceBetweenCenters = postGap + postDiameter; // 2*r + gap

            var postHolesY = Constants.JigHoleSpacing - 25f;

            var leftPostX = -distanceBetweenCenters / 2f;

            float runningX = leftPostX;
            float runningY = postHolesY;
            float accumulatedAngle = 0;

            // Flat posts and the ones to the right
            for (var i = referencePair; i < allPosts.Length; i++)
            {
                var post = allPosts[i];

                var c = Circles.EtchCircle(postDiameter + 1f, runningX, runningY);
                var h = post.GapToRight + postDiameter;

                float angle = i == referencePair ? 0 : post.AngleToRight;

                runningX += h * DegreeTrig.Cos(angle + accumulatedAngle);
                runningY -= h * DegreeTrig.Sin(angle + accumulatedAngle); // Draw them going up
                accumulatedAngle += angle;

                Children.Add(c);
            }

            runningX = leftPostX;
            runningY = postHolesY;
            accumulatedAngle = 0;

            // Ones to the left
            for (var i = referencePair - 1; i >= 0 ; i--)
            {
                var post = allPosts[i];

                var h = post.GapToRight + postDiameter;

                runningX -= h * DegreeTrig.Cos(post.AngleFromRight + accumulatedAngle); // Draw them going left
                runningY -= h * DegreeTrig.Sin(post.AngleFromRight + accumulatedAngle); // Draw them going up
                accumulatedAngle += post.AngleFromRight;

                var c = Circles.EtchCircle(postDiameter + 1f, runningX, runningY);
                
                Children.Add(c);
            }

            // spin through the posts drawing them such that the reference pair is "flat" on the bottom
            //foreach (var post in allPosts)
            //{
            //    var c = Circles.EtchCircle(postDiameter + 1f, runningX, runningY);
            //    var h = post.GapToRight + postDiameter;

            //    runningX += h * DegreeTrig.Cos(post.AngleToNext + accumulatedAngle);
            //    runningY -= h * DegreeTrig.Sin(post.AngleToNext + accumulatedAngle); // Draw them going up
            //    accumulatedAngle += post.AngleToNext;

            //    Children.Add(c);
            //}

            // Draw the neighboring posts so that we can later subtract their paths
            //foreach (var neighbor in neighbors)
            //{
            //    var acuteAngle = 180f - neighbor.AngleToNext;
            //    var h = neighbor.GapToRight + postDiameter;

            //    var y = h * DegreeTrig.Sin(acuteAngle);
            //    var x = h * DegreeTrig.Cos(acuteAngle);

            //    var centerY = postHolesY - y;
            //    float centerX;

            //    if (neighbor.OnLeft)
            //    {
            //        centerX = leftPostX - x;
            //    }
            //    else
            //    {
            //        centerX = leftPostX + distanceBetweenCenters + x;
            //    }

            //    var c = Circles.CutCircle(postDiameter + 2f, centerX, centerY); // 1mm of slop
            //    Children.Add(c);
            //}
        }
    }
}
