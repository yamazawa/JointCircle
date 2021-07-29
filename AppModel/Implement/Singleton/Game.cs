using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AppModel.IF.Singleton;
using AppModel.Implement.Stuff;
using AppModel.Stuff.IF;
using AppModel.IF.Pile;
using System.Collections.Generic;

namespace AppModel.Implement.Singleton
{
    /// <summary>ゲーム処理</summary>
    internal class Game : IGame
    {
        public IPileCollection PileCollection { get; }

        public IStuffCollection StuffCollection { get; }

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
                    StuffCollection.List.Remove(circle);
                }
                else
                {
                    circle.State = StuffState.Jointed;
                }
            }
        }

        /// <summary>指定座標をタップした場合の動作</summary>
        public void TapAction(Point point)
        {
            StuffCollection.AddCircle(point, 10, StuffState.Generating);
        }

    }
}
