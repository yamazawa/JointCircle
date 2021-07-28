using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AppModel.IF.Pile;
using AppModel.Implement.Calc;
using AppModel.Stuff.IF;

namespace AppModel.Implement.Stuff
{
    /// <summary>円(モノ)</summary>
    internal class Circle : Stuff, ICircle
    {
        public Circle(int id, IPile centerPile, IPile radiousPile) : base(id)
        {
            CenterPile = centerPile;
            RadiousPile = radiousPile;
        }

        #region Implement ICircle

        /// <summary>中心杭(円の中心)</summary>
        public IPile CenterPile { get; }

        /// <summary>半径杭(中心杭と半径杭を繋ぐ直線が円の半径となる)</summary>
        public IPile RadiousPile { get; }

        /// <summary>半径</summary>
        public double Radious
        {
            get => DistanceCalc.GetDistance(CenterPile, RadiousPile);
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
            var jointList = stuffList.Where(i => i != this && i.State == StuffState.Jointed);
            if (jointList.Count() == 0) return false;

            var mostNearDistance = jointList.Min(i => GetDistance(i));
            var isJointed = mostNearDistance <= 0;

            // 生成中で接続した場合、ピッタリに接続させるよう半径を調整する
            if (isJointed && mostNearDistance > -2 && State == StuffState.Generating)
            {
                IncreaseRadiousSize(-Math.Abs(mostNearDistance));
            }
            return isJointed;
        }

        /// <summary>障害物判定</summary>
        public override bool Obstacle(IList<IStuff> stuffList)
        {
            if (State == StuffState.NotJointed) return false;

            var obstacleList = stuffList.Where(i => i != this && i.State == StuffState.Obstacle);
            if (obstacleList.Count() == 0) return false;

            return obstacleList.Min(i => GetDistance(i)) < 0;
        }

        /// <summary>生成時の動作</summary>
        public override void Generating()
        {
            if (State != StuffState.Generating) return;
            IncreaseRadiousSize(2);
        }

        private void IncreaseRadiousSize(double delta)
        {
            RadiousPile.Position = new Point(RadiousPile.Position.X, RadiousPile.Position.Y + delta);
        }

        #endregion
    }
}
