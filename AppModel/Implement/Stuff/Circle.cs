using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AppModel.Implement.Calc;
using AppModel.Stuff.IF;

namespace AppModel.Implement.Stuff
{
    /// <summary>円(モノ)</summary>
    internal class Circle : Stuff, ICircle
    {
        public Circle(int id) : base(id)
        {
        }

        #region Implement ICircle

        /// <summary>中心点</summary>
        public Point CenterPoint
        {
            get => _centerPoint;
            set
            {
                _centerPoint = value;
                RaisePropertyChanged();
            }
        }
        private Point _centerPoint;

        /// <summary>半径</summary>
        public double Radious
        {
            get => _radious;
            set
            {
                _radious = value;
                RaisePropertyChanged();
            }
        }
        private double _radious;

        #endregion

        #region Implement IStuff

        /// <summary>指定したモノに対する距離を取得</summary>
        public override double GetDistance(IStuff targetStuff)
        {
            if (targetStuff is Circle targetCircle)
                return DistanceCalc.GetDistance(this, targetCircle);
            // それ以外の場合 TODO
            return 0;
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
                Radious -= Math.Abs(mostNearDistance);
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
            Radious += 2;
        }

        #endregion
    }
}
