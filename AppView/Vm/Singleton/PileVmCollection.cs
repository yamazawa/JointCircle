using AppModel.IF.Pile;
using AppModel.IF.Singleton;
using AppView.Vm.Pile;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace AppView.Vm.Singleton
{
    class PileVmCollection
    {
        public IList<PileVm> List { get; } = new ObservableCollection<PileVm>();

        public PileVmCollection(IPileCollection model)
        {
            model.List.CollectionChanged += PileList_CollectionChanged;
        }

        private void PileList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems.OfType<IPile>())
                    {
                        List.Add(new PileVm(item));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems.OfType<IPile>())
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
