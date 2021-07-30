using System.Collections.Generic;
using AppModel.Implement.Stuff;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AppModel.IF.Pile;
using AppModel.IF.Singleton;
using AppModel.IF.Stuff;

namespace AppModel.Implement.Singleton
{
    class StuffCollection : IStuffCollection
    {
        /// <summary>ゲーム上のモノのリスト</summary>
        public ObservableCollection<IStuff> List { get; }

        public StuffCollection(IPileCollection pileCollection)
        {
            _pileCollection = pileCollection;
            List = new ObservableCollection<IStuff>();
        }

        /// <summary>円を追加します。</summary>
        public void AddCircle(Point point, double radius, StuffState state)
        {
            var centerPile = GetNewPile(point, StateDictionary[state]);
            var radiusPile = GetNewPile(new Point(point.X, point.Y + radius), PileState.Hide);
            var newCircle = new Circle(GetNewId(), state, centerPile, radiusPile);
            newCircle.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(newCircle.State):
                        centerPile.State = StateDictionary[newCircle.State];
                        break;
                }
            };
            List.Add(newCircle);
        }

        /// <summary>直線を追加します</summary>
        public void AddLine(Point point1, Point point2, StuffState state)
        {
            var pile1 = GetNewPile(point1, StateDictionary[state]);
            var pile2 = GetNewPile(point2, StateDictionary[state]);
            var newLine = new Line(GetNewId(), state, pile1, pile2);
            newLine.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(newLine.State):
                        pile1.State = StateDictionary[newLine.State];
                        pile2.State = StateDictionary[newLine.State];
                        break;
                }
            };
            List.Add(newLine);
        }

        private readonly IPileCollection _pileCollection;

        private IPile GetNewPile(Point position, PileState state)
        {
            return _pileCollection.AddPile(position, state);
        }

        /// <summary>新しいIDを生成します。</summary>
        private int GetNewId()
        {
            return List.Count == 0
                ? 0
                : List.Max(i => i.Id) + 1;
        }

        public ReadOnlyDictionary<StuffState, PileState> StateDictionary = new ReadOnlyDictionary<StuffState, PileState>(
            new Dictionary<StuffState, PileState>()
            {
                { StuffState.Generating, PileState.Generating },
                { StuffState.Obstacle, PileState.Obstacle },
                { StuffState.NotJointed, PileState.NotJointed },
                { StuffState.Jointed, PileState.Jointed },
                { StuffState.FailedShadow, PileState.FailedShadow }
            });
    }
}
