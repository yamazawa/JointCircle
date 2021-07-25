using System.Collections.Generic;
using System.Windows;
using AppModel.Stuff.IF;

namespace AppModel.Implement.Stuff
{
    /// <summary>直線(モノ)</summary>
    internal class Line : Stuff, ILine
    {
        public Line(int id) : base(id)
        {
        }

        #region Implement ICircle

        /// <summary>点1</summary>
        public Point Point1
        {
            get => _point1;
            set
            {
                _point1 = value;
                RaisePropertyChanged();
            }
        }
        private Point _point1;

        /// <summary>点2</summary>
        public Point Point2
        {
            get => _point2;
            set
            {
                _point2 = value;
                RaisePropertyChanged();
            }
        }
        private Point _point2;

        #endregion

        #region Implement IStuff

        public override double GetDistance(IStuff targetStuff)
        {
            throw new System.NotImplementedException();
        }

        public override bool Joint(IList<IStuff> stuffList)
        {
            throw new System.NotImplementedException();
        }

        public override bool Obstacle(IList<IStuff> stuffList)
        {
            throw new System.NotImplementedException();
        }

        public override void Generating()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
