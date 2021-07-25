using AppModel.Stuff.IF;

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
            }
            return null;
        }
    }
}
