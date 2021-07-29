using System.Collections.Generic;
using AppModel.Implement.Stuff;
using AppModel.Stuff.IF;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AppModel.IF.Pile;
using AppModel.IF.Singleton;

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
        public void AddCircle(Point point, double radious, StuffState state)
        {
            var centerPile = GetNewPile(point, StateDictionary[state]);
            var radiousPile = GetNewPile(new Point(point.X, point.Y + radious), PileState.Hide);
            List.Add(new Circle(GetNewId(), centerPile, radiousPile)
            {
                State = state
            });
        }

        /// <summary>直線を追加します</summary>
        public void AddLine(Point point1, Point point2, StuffState state)
        {
            var pile1 = GetNewPile(point1, StateDictionary[state]);
            var pile2 = GetNewPile(point2, StateDictionary[state]);
            List.Add(new Line(GetNewId(), pile1, pile2)
            {
                State = state
            });
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
            });
    }
}
