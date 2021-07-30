using System.Collections.Generic;
using System.ComponentModel;

namespace AppModel.IF.Stuff
{
    /// <summary>モノ</summary>
    public interface IStuff : INotifyPropertyChanged
    {
        /// <summary>モノID</summary>
        int Id { get; }

        /// <summary>モノの状態</summary>
        StuffState State { get; set; }

        /// <summary>指定したモノに対する距離を取得します。</summary>
        double GetDistance(IStuff targetStuff);

        /// <summary>接続判定</summary>
        bool Joint(IList<IStuff> stuffList);

        /// <summary>障害物判定</summary>
        bool Obstacle(IList<IStuff> stuffList);

        /// <summary>生成動作</summary>
        void Generating();
    }

    public enum StuffState
    {
        /// <summary>生成中</summary>
        Generating,
        /// <summary>未接続</summary>
        NotJointed,
        /// <summary>接続した状態</summary>
        Jointed,
        /// <summary>障害物</summary>
        Obstacle,
        /// <summary>失敗（跡が薄く残っている状態）</summary>
        FailedShadow
    }
}
