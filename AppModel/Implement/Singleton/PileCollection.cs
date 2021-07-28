using AppModel.IF.Pile;
using AppModel.IF.Singleton;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AppModel.Implement.Singleton
{
    internal class PileCollection : IPileCollection
    {
        /// <summary>ゲーム上の杭のリスト</summary>
        public ObservableCollection<IPile> PileList { get; }

        public PileCollection()
        {
            PileList = new ObservableCollection<IPile>();
        }

        /// <summary>杭をリストに追加します</summary>
        public IPile AddPile(Point position, PileState state)
        {
            var newPile = new Pile.Pile(GetNewId())
            {
                Position = position,
                State = state
            };
            PileList.Add(newPile);
            return newPile;
        }

        /// <summary>杭を削除します</summary>
        public void RemovePile(IPile pile)
        {
            PileList.Remove(pile);
        }

        /// <summary>新しいIDを生成します。</summary>
        private int GetNewId()
        {
            return PileList.Count == 0
                   ? 0
                   : PileList.Max(i => i.Id) + 1;
        }
    }
}
