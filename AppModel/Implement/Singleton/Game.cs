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
        /// <summary>現在のモノのIDの最大値</summary>
        private int _idMaxCount = 0;

        public IPileCollection PileCollection { get; }

        /// <summary>ゲーム上に描画されるモノのリスト</summary>
        public ObservableCollection<IStuff> StuffList { get; set; } = new ObservableCollection<IStuff>();

        public Game()
        {
            PileCollection = new PileCollection();
        }

        /// <summary>ゲームを初期化します</summary>
        public void Initialize()
        {
            AddCircle(new Point(150, 150), 100, StuffState.Jointed);
            AddCircle(new Point(450, 50), 100, StuffState.Obstacle);
            AddCircle(new Point(450, 350), 100, StuffState.Obstacle);
            AddCircle(new Point(600, 150), 100, StuffState.NotJointed);
            AddLine(new Point(150, 50), new Point(600, 50), StuffState.Obstacle);
            AddLine(new Point(150, 350), new Point(600, 350), StuffState.Obstacle);
        }

        /// <summary>フレーム更新毎の更新処理</summary>
        public void FrameUpdate()
        {
            foreach (var circle in StuffList.Where(i => i.State == StuffState.Generating || i.State == StuffState.NotJointed).ToList())
            {
                // 生成中の円を膨張させます
                circle.Generating();
                if (!circle.Joint(StuffList)) continue;

                if (circle.Obstacle(StuffList))
                {
                    StuffList.Remove(circle);
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
            AddCircle(point, 10, StuffState.Generating);
        }

        /// <summary>円を追加します。</summary>
        private void AddCircle(Point point, double radious, StuffState state)
        {
            var centerPile = GetNewPile(point, StateDictionary[state]);
            var radiousPile = GetNewPile(new Point(point.X, point.Y + radious), PileState.Hide);
            _idMaxCount += 1;
            StuffList.Add(new Circle(_idMaxCount, centerPile, radiousPile)
            {
                State = state
            });
        }

        private void AddLine(Point point1, Point point2, StuffState state)
        {
            var pile1 = GetNewPile(point1, StateDictionary[state]);
            var pile2 = GetNewPile(point2, StateDictionary[state]);
            _idMaxCount += 1;
            StuffList.Add(new Line(_idMaxCount, pile1, pile2)
            {
                State = state
            });
            
        }

        private IPile GetNewPile(Point position, PileState state)
        {
            return PileCollection.AddPile(position, state);
        }

        public ReadOnlyDictionary<StuffState, PileState> StateDictionary = new ReadOnlyDictionary<StuffState, PileState>(
            new Dictionary<StuffState, PileState>()
            {
                { StuffState.Generating, PileState.Generating },
                { StuffState.Obstacle, PileState.Obstacle },
                { StuffState.NotJointed, PileState.NotJointed },
                { StuffState.Jointed, PileState.Jointed },
            });
    }
}
