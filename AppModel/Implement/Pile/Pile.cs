using AppModel.IF.Pile;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AppModel.Implement.Pile
{
    /// <summary>杭</summary>
    internal class Pile : IPile
    {
        /// <summary>杭のID</summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }
        private int _id;

        /// <summary>座標</summary>
        public Point Position
        {
            get => _position;
            set
            {
                _position = value;
                RaisePropertyChanged();
            }
        }
        private Point _position;

        #region INotifyPropertyChanged実装
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
