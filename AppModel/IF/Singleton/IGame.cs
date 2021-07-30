using System.Windows;

namespace AppModel.IF.Singleton
{
    /// <summary>ゲーム処理</summary>
    public interface IGame
    {
        /// <summary>ゲーム上の杭リスト</summary>
        IPileCollection PileCollection { get; }

        /// <summary>ゲーム上のモノリスト</summary>
        IStuffCollection StuffCollection { get; }

        /// <summary>ゲームを初期化します</summary>
        void Initialize();

        /// <summary>フレーム更新毎の更新処理</summary>
        void FrameUpdate();

        /// <summary>指定座標にMouseDownした場合の動作</summary>
        void MouseDownAction(Point point);

        /// <summary>指定座標にMouseMoveした場合の動作</summary>
        void MouseMoveAction(Point point);

        /// <summary>指定座標にMouseUpした場合の動作</summary>
        void MouseUpAction(Point point);
    }
}
