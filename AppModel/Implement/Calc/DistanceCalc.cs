using System;
using AppModel.Stuff.IF;

namespace AppModel.Implement.Calc
{
    internal static class DistanceCalc
    {
        public static double GetDistance(ICircle circle1, ICircle circle2)
        {
            var centerVector = circle1.CenterPoint - circle2.CenterPoint;

            var centerDistance = Math.Sqrt(Math.Pow(centerVector.X, 2) + Math.Pow(centerVector.Y, 2));
            var bigRadious = Math.Max(circle1.Radious, circle2.Radious);
            var smallRadious = Math.Min(circle1.Radious, circle2.Radious);

            return centerDistance - bigRadious > 0
                   ? centerDistance - (bigRadious + smallRadious)
                   : bigRadious - (centerDistance + smallRadious);
        }
    }
}
