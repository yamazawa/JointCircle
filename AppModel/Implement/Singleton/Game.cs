using System.Linq;
using System.Windows;
using AppModel.IF.Pile;
using AppModel.IF.Singleton;
using AppModel.IF.Stuff;

namespace AppModel.Implement.Singleton
{
    /// <summary>ゲーム処理</summary>
    internal class Game : IGame
    {

        /// <summary>杭のコレクション</summary>
        public IPileCollection PileCollection { get; }

        /// <summary>モノのコレクション</summary>
        public IStuffCollection StuffCollection { get; }

        /// <summary>ドラッグ中の杭</summary>
        private IPile OnDragPile;

        public Game()
        {
            PileCollection = new PileCollection();
            StuffCollection = new StuffCollection(PileCollection);
        }

        /// <summary>ゲームを初期化します</summary>
        public void Initialize()
        {
            StuffCollection.AddCircle(new Point(150, 150), 100, StuffState.Jointed);
            StuffCollection.AddCircle(new Point(450, 50), 100, StuffState.Obstacle);
            StuffCollection.AddCircle(new Point(450, 350), 100, StuffState.Obstacle);
            StuffCollection.AddCircle(new Point(600, 150), 100, StuffState.NotJointed);
            StuffCollection.AddLine(new Point(150, 50), new Point(600, 50), StuffState.Obstacle);
            StuffCollection.AddLine(new Point(150, 350), new Point(600, 350), StuffState.Obstacle);
        }

        /// <summary>フレーム更新毎の更新処理</summary>
        public void FrameUpdate()
        {
            foreach (var circle in StuffCollection.List.Where(i => i.State == StuffState.Generating || i.State == StuffState.NotJointed).ToList())
            {
                // 生成中の円を膨張させます
                circle.Generating();
                if (!circle.Joint(StuffCollection.List)) continue;

                if (circle.Obstacle(StuffCollection.List))
                {
                    circle.State = StuffState.FailedShadow;
                }
                else
                {
                    circle.State = StuffState.Jointed;
                }
            }
        }

        private Point _tapedPosition;

        private Point _beforeTapPilePosition;

        /// <summary>指定座標をタップした場合の動作</summary>
        public void MouseDownAction(Point position)
        {
            var touchedPile = PileCollection.GetTouchedPile(position);
            if (touchedPile != null)
            {
                OnDragPile = touchedPile;
                _tapedPosition = position;
                _beforeTapPilePosition = touchedPile.Position;
                return;
            }
            StuffCollection.AddCircle(position, 10, StuffState.Generating);
        }

        public void MouseMoveAction(Point position)
        {
            if (OnDragPile == null) return;
            var x = _beforeTapPilePosition.X + position.X - _tapedPosition.X;
            var y = _beforeTapPilePosition.Y + position.Y - _tapedPosition.Y;
            OnDragPile.Position = new Point(x, y);
        }

        public void MouseUpAction(Point position)
        {
            OnDragPile = null;
        }
    }
}
