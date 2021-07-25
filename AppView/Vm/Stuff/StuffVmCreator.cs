using AppModel.Stuff.IF;
using System.Diagnostics;

namespace AppView.Vm.Stuff
{
    public class StuffVmCreator
    {
        public StuffVm Create(IStuff model)
        {
            switch (model)
            {
                case ICircle circle:
                    return new CircleVm(circle);
                case ILine line:
                    return new LineVm(line);
            }
            Debug.Assert(false, "not defined.");
            return null;
        }
    }
}
