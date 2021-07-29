using AppModel.IF.Pile;
using System.Collections.ObjectModel;
using System.Windows;

namespace AppModel.IF.Singleton
{
    public interface IPileCollection
    {
        /// <summary>ゲーム上の杭のリスト</summary>
        ObservableCollection<IPile> List { get; }

        /// <summary>杭をリストに追加します</summary>
        IPile AddPile(Point position, PileState state);

        /// <summary>杭を削除します</summary>
        void RemovePile(IPile pile);
    }
}
