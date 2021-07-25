using System.ComponentModel;
using System.Windows;
using AppModel.Stuff.IF;

namespace AppView.Vm.Stuff
{
    /// <summary>円(モノ)</summary>
    public class CircleVm : StuffVm
    {
        private new ICircle Model => (ICircle)base.Model;

        public CircleVm(ICircle model) : base(model)
        {
        }

        /// <summary>中心点</summary>
        public Point CenterPoint
        {
            get => Model.CenterPoint;
        }

        /// <summary>半径</summary>
        public double Radious
        {
            get => Model.Radious;
        }

        /// <summary>左上座標</summary>
        public Point LeftUpPoint => new Point(CenterPoint.X - Radious, CenterPoint.Y - Radious);

        /// <summary>高さ</summary>
        public double Height => Radious * 2;

        /// <summary>横幅</summary>
        public double Width => Radious * 2;

        /// <summary>Model側の値が変更された時の動作</summary>
        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.Model_PropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Model.Radious):
                    RaisePropertyChanged(nameof(Radious));
                    RaisePropertyChanged(nameof(LeftUpPoint));
                    RaisePropertyChanged(nameof(Height));
                    RaisePropertyChanged(nameof(Width));
                    break;
                case nameof(Model.CenterPoint):
                    RaisePropertyChanged(nameof(LeftUpPoint));
                    RaisePropertyChanged(nameof(CenterPoint));
                    break;
            }
        }
    }
}
