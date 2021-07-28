using System.ComponentModel;
using System.Windows;

namespace AppModel.IF.Pile
{
    /// <summary>杭</summary>
    public interface IPile : INotifyPropertyChanged
    {
        /// <summary>杭のID</summary>
        int Id { get; }

        /// <summary>座標</summary>
        Point Position { get; set; }

        PileState State { get; set; }
    }

    public enum PileState
    {
        /// <summary>生成中</summary>
        Generating,
        /// <summary>未接続</summary>
        NotJointed,
        /// <summary接続した状態</summary>
        Jointed,
        /// <summary>非表示</summary>
        Hide,
        /// <summary>影状態</summary>
        Shadow,
        /// <summary>障害物</summary>
        Obstacle,
    }
}
