using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Antidote.Utility
{
    static class Vectors
    {

        public static Vector2 Rotate(Vector2 aPoint, float aDegree)
        {
            var theta = ToRadians(aDegree);
            var x = aPoint.X;
            var y = aPoint.Y;

            var cs = (float)Math.Cos(theta);
            var sn = (float)Math.Sin(theta);

            var result = new Vector2();
            result.X = (float)Math.Round(x * cs - y * sn);
            result.Y = (float)Math.Round(x * sn + y * cs);

            return result;
        }

        public static float ToRadians(float val)
        {
            return (float)(Math.PI / 180) * val;
        }
    }
}
