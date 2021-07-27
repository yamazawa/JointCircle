using AppModel.Stuff.IF;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppModel.IF.Singleton
{
    /// <summary>ゲーム処理</summary>
    public interface IGame
    {
        /// <summary>ゲーム上の杭リスト</summary>
        IPileCollection PileCollection { get; }

        /// <summary>ゲーム上に描画されるモノのリスト</summary>
        ObservableCollection<IStuff> StuffList { get; set; }

        /// <summary>ゲームを初期化します</summary>
        void Initialize();

        /// <summary>フレーム更新毎の更新処理</summary>
        void FrameUpdate();

        /// <summary>指定座標をタップした場合の動作</summary>
        void TapAction(Point point);
    }
}
