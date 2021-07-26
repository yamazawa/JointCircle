using System;
using System.Diagnostics;
using System.Windows;
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
            var centerDistance = Abs(circle1.CenterPoint - circle2.CenterPoint);
            var bigRadious = Math.Max(circle1.Radious, circle2.Radious);
            var smallRadious = Math.Min(circle1.Radious, circle2.Radious);

            return centerDistance - bigRadious > 0
                   ? centerDistance - (bigRadious + smallRadious)
                   : bigRadious - (centerDistance + smallRadious);
        }

        private static double GetDistanceOfCircleAndLine(ICircle circle, ILine line)
        {
            var A = line.Point1;
            var B = line.Point2;
            var O = circle.CenterPoint;
            var r = circle.Radious;
            var AO = A - O;
            var BO = B - O;

            // 円の内側に点A点Bがある場合
            if (Math.Max(Abs(AO), Abs(BO)) < r)
                return r - Math.Max(Abs(AO), Abs(BO));

            // 円の外側に点A点Bがある場合
            else if (Math.Min(Abs(AO), Abs(BO)) >= r)
            {
                var P = GetBelowPerpendicular(A, B, O);
                var PO = P - O;
                return Math.Min(Math.Min(Abs(AO), Abs(BO)), Abs(PO)) - r;
            }

            // 上記以外(点A点Bの一方が円の外側、もう一方が円の内側の場合)
            return Math.Min(Abs(AO), Abs(BO)) - r;
        }

        private static double GetDistanceOfLines(ILine line1, ILine line2)
        {
            // TODO
            return 0;
        }

        private static double Abs(Vector v)
        {
            return Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
        }

        /// <summary>点Oから直線AB上に垂線を下した位置Pを求める</summary>
        private static Point GetBelowPerpendicular(Point A, Point B, Point O)
        {
            // AB⇒aベクトル、AO⇒bベクトルと定義する。
            // AP⇒t*aベクトルと定義してtの値を求める。(ここは数学で演算した。)
            var a1 = B.X - A.X;
            var a2 = B.Y - A.Y;
            var b1 = O.X - A.X;
            var b2 = O.Y - A.Y;
            var t = Math.Pow(b1, 2) == Math.Pow(b2, 2)
                    ? (b1 + b2) / (2 * a1)
                    : (a1 * b1 - a2 * b2) / (Math.Pow(a1, 2) - Math.Pow(a2, 2));
            return new Point(A.X + t * a1, A.Y + t * a2);
        }
    }
}
