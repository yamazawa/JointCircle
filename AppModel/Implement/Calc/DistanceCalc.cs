using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using AppModel.IF.Pile;
using AppModel.IF.Stuff;

namespace AppModel.Implement.Calc
{
    internal static class DistanceCalc
    {
        public static double GetDistance(IPile pile1, IPile pile2)
        {
            return Abs(pile1.Position - pile2.Position);
        }

        public static double GetDistance(IStuff stuff1, IStuff stuff2)
        {
            if (stuff2 is ICircle circle)
                return GetDistance(stuff1, circle);
            if (stuff2 is ILine line)
                return GetDistance(stuff1, line);
            Debug.Assert(false, "Not defined Calc.");
            return 0;
        }

        private static double GetDistance(IStuff stuff, ICircle circle)
        {
            if (stuff is ICircle c)
                return GetDistanceOfCircles(c, circle);
            if (stuff is ILine l)
                return GetDistanceOfCircleAndLine(circle, l);
            Debug.Assert(false, "Not defined Calc.");
            return 0;
        }

        private static double GetDistance(IStuff stuff, ILine line)
        {
            if (stuff is ICircle c)
                return GetDistanceOfCircleAndLine(c, line);
            if (stuff is ILine l)
                return GetDistanceOfLines(l, line);
            Debug.Assert(false, "Not defined Calc.");
            return 0;
        }

        private static double GetDistanceOfCircles(ICircle circle1, ICircle circle2)
        {
            var centerDistance = GetDistance(circle1.CenterPile, circle2.CenterPile);
            var bigRadius = Math.Max(circle1.Radius, circle2.Radius);
            var smallRadius = Math.Min(circle1.Radius, circle2.Radius);

            return centerDistance - bigRadius > 0
                   ? centerDistance - (bigRadius + smallRadius)
                   : bigRadius - (centerDistance + smallRadius);
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private static double GetDistanceOfCircleAndLine(ICircle circle, ILine line)
        {
            // lineの直線を結ぶ点を点A、点B、circleの円の中心を点Oとする。
            var A = line.Pile1.Position;
            var B = line.Pile2.Position;
            var O = circle.CenterPile.Position;
            var r = circle.Radius;
            var AO = A - O;
            var BO = B - O;

            // 円の内側に点A点Bがある場合
            if (Max(Abs(AO), Abs(BO)) < r)
                return r - Max(Abs(AO), Abs(BO));

            // 円の外側に点A点Bがある場合
            else if (Min(Abs(AO), Abs(BO)) >= r)
            {
                var (P, isOnAB) = GetBelowPerpendicular(A, B, O);
                var PO = P - O;
                return isOnAB
                       ? Abs(PO) - r
                       : Math.Min(Abs(AO), Abs(BO)) - r;
            }

            // 上記以外(点A点Bの一方が円の外側、もう一方が円の内側の場合)
            return Min(Abs(AO), Abs(BO)) - r;
        }

        
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static double GetDistanceOfLines(ILine line1, ILine line2)
        {
            // TODO
            return 0;
        }

        /// <summary>点Oから直線AB上に垂線を下した位置Pを求める</summary>
        /// <returns>(点Pの座標, 点Pが直線AB上にあるかどうか)</returns>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private static (Point, bool) GetBelowPerpendicular(Point A, Point B, Point O)
        {
            // AB⇒aベクトル、AO⇒bベクトルと定義する。
            // AP⇒t*aベクトルと定義してtの値を求める。(ここは数学で演算した。)
            var a1 = B.X - A.X;
            var a2 = B.Y - A.Y;
            var b1 = O.X - A.X;
            var b2 = O.Y - A.Y;
            var t = Math.Abs(Pow2(b1) - Pow2(b2)) < double.Epsilon
                    ? (b1 + b2) / (2 * a1)
                    : (a1 * b1 - a2 * b2) / (Pow2(a1) - Pow2(a2));
            // P⇒ A + AP ⇒ A + t*a
            var P = new Point(A.X + t * a1, A.Y + t * a2);
            return (P, 0 <= t && t <= 1);
        }

        private static double Abs(Vector v)
        {
            return Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
        }

        private static double Max(params double[] v)
        {
            return v.Max(i => i);
        }

        private static double Min(params double[] v)
        {
            return v.Min(i => i);
        }

        private static double Pow2(double t)
        {
            return Math.Pow(t, 2);
        }
    }
}
