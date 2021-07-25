using AppModel.Stuff.IF;
using System.ComponentModel;
using System.Windows;

namespace AppView.Vm.Stuff
{
    public class LineVm : StuffVm
    {
        private new ILine Model => (ILine)base.Model;

        public LineVm(ILine model) : base(model)
        {
        }

        /// <summary>点1</summary>
        public Point Point1 => Model.Point1;

        /// <summary>点2</summary>
        public Point Point2 => Model.Point2;

        /// <summary>Model側の値が変更された時の動作</summary>
        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.Model_PropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Model.Point1):
                    RaisePropertyChanged(nameof(Point1));
                    break;
                case nameof(Model.Point2):
                    RaisePropertyChanged(nameof(Point2));
                    break;
            }
        }
    }
}
