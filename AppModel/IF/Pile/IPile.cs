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

        /// <summary>杭の状態</summary>
        PileState State { get; set; }
    }

    public enum PileState
    {
        /// <summary>生成中</summary>
        Generating,
        /// <summary>未接続</summary>
        NotJointed,
        /// <summary>接続</summary>
        Jointed,
        /// <summary>非表示</summary>
        Hide,
        /// <summary>障害物</summary>
        Obstacle,
        /// <summary>失敗（跡が薄く残っている状態）</summary>
        FailedShadow
    }
}
