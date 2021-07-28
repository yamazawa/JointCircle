using AppModel.IF.Pile;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AppView.Vm.Pile
{
    /// <summary>杭</summary>
    public class PileVm : INotifyPropertyChanged
    {
        protected IPile Model;

        public PileVm(IPile model)
        {
            Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>杭のID</summary>
        public int Id
        {
            get => Model.Id;
        }

        /// <summary>座標</summary>
        public Point Position
        {
            get => Model.Position;
        }

        public PileState State
        {
            get => Model.State;
        }

        /// <summary>Model側の値が変更された時の動作</summary>
        protected virtual void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.Position):
                    RaisePropertyChanged(nameof(Position));
                    break;
                case nameof(Model.State):
                    RaisePropertyChanged(nameof(State));
                    break;
            }
        }

        #region INotifyPropertyChanged実装
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
