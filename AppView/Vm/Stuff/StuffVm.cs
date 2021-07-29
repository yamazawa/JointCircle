using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AppModel.IF.Stuff;

namespace AppView.Vm.Stuff
{
    /// <summary>モノ</summary>
    public abstract class StuffVm : INotifyPropertyChanged
    {
        protected IStuff Model;

        public StuffVm(IStuff model)
        {
            Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>ID(モノに一意に振られるID)</summary>
        public int Id => Model.Id;

        /// <summary>左上座標</summary>
        /// <remarks>(CanvasのTopLeftを使用したい場合にOverrideすること)</remarks>
        public virtual Point LeftUpPoint => new Point(0, 0);

        /// <summary>ステータス</summary>
        public StuffState State => Model.State;

        /// <summary>Model側の値が変更された時の動作</summary>
        protected virtual void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
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
