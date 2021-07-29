using AppModel.IF.Pile;

namespace AppModel.IF.Stuff
{
    /// <summary>円(モノ)</summary>
    public interface ICircle : IStuff
    {
        /// <summary>中心杭(円の中心)</summary>
        IPile CenterPile { get; }

        /// <summary>半径杭(中心杭と半径杭を繋ぐ直線が円の半径となる)</summary>
        IPile RadiusPile { get; }

        /// <summary>半径</summary>
        double Radius { get; }
    }
}
