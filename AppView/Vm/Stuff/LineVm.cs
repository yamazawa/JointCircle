using AppModel.Stuff.IF;
using AppView.Vm.Pile;

namespace AppView.Vm.Stuff
{
    public class LineVm : StuffVm
    {
        private new ILine Model => (ILine)base.Model;

        public LineVm(ILine model) : base(model)
        {
            Pile1 = new PileVm(Model.Pile1);
            Pile2 = new PileVm(Model.Pile2);
        }

        /// <summary>杭1</summary>
        public PileVm Pile1 { get; }

        /// <summary>杭2</summary>
        public PileVm Pile2 { get; }
    }
}
