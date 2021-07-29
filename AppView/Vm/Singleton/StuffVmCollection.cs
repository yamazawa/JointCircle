using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using AppModel.IF.Singleton;
using AppModel.IF.Stuff;
using AppView.Vm.Stuff;

namespace AppView.Vm.Singleton
{
    class StuffVmCollection
    {
        public IList<StuffVm> List { get; } = new ObservableCollection<StuffVm>();

        public StuffVmCollection(IStuffCollection model)
        {
            model.List.CollectionChanged += List_CollectionChanged;
        }

        private void List_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var creator = new StuffVmCreator();
                    foreach (var item in e.NewItems.OfType<IStuff>())
                    {
                        List.Add(creator.Create(item));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems.OfType<IStuff>())
                    {
                        var removeItem = List.FirstOrDefault(i => i.Id == item.Id);
                        List.Remove(removeItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    List.Clear();
                    break;
            }
        }
    }
}
