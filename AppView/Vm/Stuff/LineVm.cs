using AppModel.Stuff.IF;
using AppView.Vm.Pile;
using System.ComponentModel;

namespace AppView.Vm.Stuff
{
    public class LineVm : StuffVm
    {
        private new ILine Model => (ILine)base.Model;

        public LineVm(ILine model) : base(model)
        {
            Pile1 = new PileVm(Model.Pile1);
            Pile2 = new PileVm(Model.Pile2);

            Model.Pile1.PropertyChanged += Pile1_PropertyChanged;
            Model.Pile2.PropertyChanged += Pile2_PropertyChanged;
        }

        /// <summary>杭1</summary>
        public PileVm Pile1 { get; }

        /// <summary>杭2</summary>
        public PileVm Pile2 { get; }

        private void Pile1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.Pile1.Position):
                    RaisePropertyChanged(nameof(Pile1));
                    break;
            }
        }

        private void Pile2_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.Pile2.Position):
                    RaisePropertyChanged(nameof(Pile2));
                    break;
            }
        }
    }
}
