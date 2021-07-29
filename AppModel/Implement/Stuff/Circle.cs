using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AppModel.IF.Pile;
using AppModel.IF.Stuff;
using AppModel.Implement.Calc;

namespace AppModel.Implement.Stuff
{
    /// <summary>円(モノ)</summary>
    internal class Circle : Stuff, ICircle
    {
        public Circle(int id, IPile centerPile, IPile radiusPile) : base(id)
        {
            CenterPile = centerPile;
            RadiusPile = radiusPile;
        }

        #region Implement ICircle

        /// <summary>中心杭(円の中心)</summary>
        public IPile CenterPile { get; }

        /// <summary>半径杭(中心杭と半径杭を繋ぐ直線が円の半径となる)</summary>
        public IPile RadiusPile { get; }

        /// <summary>半径</summary>
        public double Radius
        {
            get => DistanceCalc.GetDistance(CenterPile, RadiusPile);
        }

        #endregion

        #region Implement IStuff

        /// <summary>指定したモノに対する距離を取得</summary>
        public override double GetDistance(IStuff targetStuff)
        {
            return DistanceCalc.GetDistance(this, targetStuff);
        }

        /// <summary>接続判定</summary>
        public override bool Joint(IList<IStuff> stuffList)
        {
            var jointList = stuffList.Where(i => i != this && i.State == StuffState.Jointed).ToList();
            if (!jointList.Any()) return false;

            var mostNearDistance = jointList.Min(GetDistance);
            var isJointed = mostNearDistance <= 0;

            // 生成中で接続した場合、ピッタリに接続させるよう半径を調整する
            if (isJointed && mostNearDistance > -2 && State == StuffState.Generating)
            {
                IncreaseRadiusSize(-Math.Abs(mostNearDistance));
            }
            return isJointed;
        }

        /// <summary>障害物判定</summary>
        public override bool Obstacle(IList<IStuff> stuffList)
        {
            if (State == StuffState.NotJointed) return false;

            var obstacleList = stuffList.Where(i => i != this && i.State == StuffState.Obstacle).ToList();
            if (!obstacleList.Any()) return false;

            return obstacleList.Min(GetDistance) < 0;
        }

        /// <summary>生成時の動作</summary>
        public override void Generating()
        {
            if (State != StuffState.Generating) return;
            IncreaseRadiusSize(2);
        }

        private void IncreaseRadiusSize(double delta)
        {
            RadiusPile.Position = new Point(RadiusPile.Position.X, RadiusPile.Position.Y + delta);
        }

        #endregion
    }
}
