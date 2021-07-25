using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AppModel.Stuff.IF;

namespace AppView.Vm
{
    /// <summary>円のオブジェクトクラス</summary>
    class CircleVm : INotifyPropertyChanged
    {
        private ICircle _model;

        public CircleVm(ICircle model)
        {
            _model = model;
            _model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>ID(Stuffに一意に振られるID)</summary>
        public int Id => _model.Id;

        /// <summary>中心点</summary>
        public Point CenterPoint
        {
            get => _model.CenterPoint;
        }

        /// <summary>半径</summary>
        public double Radious
        {
            get => _model.Radious;
        }

        public StuffState State
        {
            get => _model.State;
        }

        /// <summary>左上座標</summary>
        public Point LeftUpPoint => new Point(CenterPoint.X - Radious, CenterPoint.Y - Radious);

        /// <summary>高さ</summary>
        public double Height => Radious * 2;

        /// <summary>横幅</summary>
        public double Width => Radious * 2;

        /// <summary>Model側の値が変更された時の動作</summary>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_model.Radious):
                    RaisePropertyChanged(nameof(Radious));
                    RaisePropertyChanged(nameof(LeftUpPoint));
                    RaisePropertyChanged(nameof(Height));
                    RaisePropertyChanged(nameof(Width));
                    break;
                case nameof(_model.CenterPoint):
                    RaisePropertyChanged(nameof(LeftUpPoint));
                    RaisePropertyChanged(nameof(CenterPoint));
                    break;
                case nameof(_model.State):
                    RaisePropertyChanged(nameof(State));
                    break;
            }
        }

        #region INotifyPropertyChanged実装
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
