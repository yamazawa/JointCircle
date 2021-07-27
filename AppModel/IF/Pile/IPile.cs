using System.ComponentModel;
using System.Windows;

namespace AppModel.IF.Pile
{
    /// <summary>杭</summary>
    public interface IPile : INotifyPropertyChanged
    {
        /// <summary>杭のID</summary>
        int Id { get; }

        /// <summary>座標</summary>
        Point Position { get; set; }
    }
}
