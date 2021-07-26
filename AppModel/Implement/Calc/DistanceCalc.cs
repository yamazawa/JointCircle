using System;
using System.Diagnostics;
using AppModel.Stuff.IF;

namespace AppModel.Implement.Calc
{
    internal static class DistanceCalc
    {
        public static double GetDistance(IStuff stuff1, IStuff stuff2)
        {
            if (stuff2 is ICircle circle)
                return GetDistance(stuff1, circle);
            if (stuff2 is ILine line)
                return GetDistance(stuff1, line);
            Debug.Assert(false, "Not defined Clac.");
            return 0;
        }

        private static double GetDistance(IStuff stuff, ICircle circle)
        {
            if (stuff is ICircle c)
                return GetDistanceOfCircles(c, circle);
            if (stuff is ILine l)
                return GetDistanceOfCircleAndLine(circle, l);
            Debug.Assert(false, "Not defined Clac.");
            return 0;
        }

        private static double GetDistance(IStuff stuff, ILine line)
        {
            if (stuff is ICircle c)
                return GetDistanceOfCircleAndLine(c, line);
            if (stuff is ILine l)
                return GetDistanceOfLines(l, line);
            Debug.Assert(false, "Not defined Clac.");
            return 0;
        }

        private static double GetDistanceOfCircles(ICircle circle1, ICircle circle2)
        {
            var centerVector = circle1.CenterPoint - circle2.CenterPoint;

            var centerDistance = Math.Sqrt(Math.Pow(centerVector.X, 2) + Math.Pow(centerVector.Y, 2));
            var bigRadious = Math.Max(circle1.Radious, circle2.Radious);
            var smallRadious = Math.Min(circle1.Radious, circle2.Radious);

            return centerDistance - bigRadious > 0
                   ? centerDistance - (bigRadious + smallRadious)
                   : bigRadious - (centerDistance + smallRadious);
        }

        private static double GetDistanceOfCircleAndLine(ICircle circle, ILine line)
        {
            // TODO
            return 0;
        }

        private static double GetDistanceOfLines(ILine line1, ILine line2)
        {
            // TODO
            return 0;
        }
    }
}
