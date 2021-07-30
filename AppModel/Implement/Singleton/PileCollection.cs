using AppModel.IF.Pile;
using AppModel.IF.Singleton;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AppModel.Implement.Calc;

namespace AppModel.Implement.Singleton
{
    internal class PileCollection : IPileCollection
    {
        /// <summary>ゲーム上の杭のリスト</summary>
        public ObservableCollection<IPile> List { get; }

        public PileCollection()
        {
            List = new ObservableCollection<IPile>();
        }

        /// <summary>杭をリストに追加します</summary>
        public IPile AddPile(Point position, PileState state)
        {
            var newPile = new Pile.Pile(GetNewId())
            {
                Position = position,
                State = state
            };
            List.Add(newPile);
            return newPile;
        }

        /// <summary>杭を削除します</summary>
        public void RemovePile(IPile pile)
        {
            List.Remove(pile);
        }

        /// <summary>指定された座標に杭がある場合、その杭を返します。無い場合はNULLを返します</summary>
        public IPile GetTouchedPile(Point position)
        {
            foreach (var pile in List)
            {
                if (DistanceCalc.GetDistance(pile.Position, position) < 7)
                    return pile;
            }
            return null;
        }

        /// <summary>新しいIDを生成します。</summary>
        private int GetNewId()
        {
            return List.Count == 0
                   ? 0
                   : List.Max(i => i.Id) + 1;
        }
    }
}
