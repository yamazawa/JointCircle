using System.Collections.ObjectModel;
using System.Windows;
using AppModel.Stuff.IF;

namespace AppModel.IF.Singleton
{
    public interface IStuffCollection
    {
        /// <summary>ゲーム上のモノのリスト</summary>
        ObservableCollection<IStuff> List { get; }

        /// <summary>円を追加します。</summary>
        void AddCircle(Point point, double radious, StuffState state);

        /// <summary>直線を追加します</summary>
        void AddLine(Point point1, Point point2, StuffState state);
    }
}
