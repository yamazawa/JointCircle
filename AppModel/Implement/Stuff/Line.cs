using System.Collections.Generic;
using AppModel.IF.Pile;
using AppModel.IF.Stuff;

namespace AppModel.Implement.Stuff
{
    /// <summary>直線(モノ)</summary>
    internal class Line : Stuff, ILine
    {
        public Line(int id, StuffState state, IPile pile1, IPile pile2) : base(id, state)
        {
            Pile1 = pile1;
            Pile2 = pile2;
        }

        #region Implement ILine

        /// <summary>杭1</summary>
        public IPile Pile1 { get; }

        /// <summary>杭2</summary>
        public IPile Pile2 { get; }

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
