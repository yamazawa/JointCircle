using System.ComponentModel;
using System.Windows;
using AppModel.Stuff.IF;
using AppView.Vm.Pile;

namespace AppView.Vm.Stuff
{
    /// <summary>円(モノ)</summary>
    public class CircleVm : StuffVm
    {
        private new ICircle Model => (ICircle)base.Model;

        public CircleVm(ICircle model) : base(model)
        {
            CenterPile = new PileVm(Model.CenterPile);
            RadiousPile = new PileVm(Model.RadiousPile);

            Model.CenterPile.PropertyChanged += CenterPile_PropertyChanged;
            Model.RadiousPile.PropertyChanged += RadiousPile_PropertyChanged;
        }

        /// <summary>中心杭(円の中心)</summary>
        public PileVm CenterPile { get; }

        /// <summary>半径杭(中心杭と半径杭を繋ぐ直線が円の半径となる)</summary>
        public PileVm RadiousPile { get; }

        /// <summary>半径</summary>
        public double Radious => Model.Radious;

        /// <summary>左上座標</summary>
        public override Point LeftUpPoint => new Point(CenterPile.Position.X - Radious, CenterPile.Position.Y - Radious);

        /// <summary>高さ</summary>
        public double Height => Radious * 2;

        /// <summary>横幅</summary>
        public double Width => Radious * 2;

        private void CenterPile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.CenterPile.Position):
                    RaisePropertyChanged(nameof(LeftUpPoint));
                    RaisePropertyChanged(nameof(CenterPile));
                    break;
            }
        }

        private void RadiousPile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.RadiousPile.Position):
                    RaisePropertyChanged(nameof(Radious));
                    RaisePropertyChanged(nameof(LeftUpPoint));
                    RaisePropertyChanged(nameof(Height));
                    RaisePropertyChanged(nameof(Width));
                    break;
            }
        }
    }
}
