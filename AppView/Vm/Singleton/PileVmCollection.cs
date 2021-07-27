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
        private IPileCollection _model;

        public IList<PileVm> PileList { get; set; } = new ObservableCollection<PileVm>();

        public PileVmCollection(IPileCollection model)
        {
            _model = model;
            _model.PileList.CollectionChanged += PileList_CollectionChanged;
        }

        private void PileList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems.OfType<IPile>())
                    {
                        PileList.Add(new PileVm(item));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems.OfType<IPile>())
                    {
                        var removeItem = PileList.FirstOrDefault(i => i.Id == item.Id);
                        PileList.Remove(removeItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    PileList.Clear();
                    break;
            }
        }
    }
}
