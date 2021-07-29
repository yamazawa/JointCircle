using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Specialized;
using AppModel;
using AppModel.Stuff.IF;
using AppModel.IF.Singleton;
using AppView.Vm.Stuff;

namespace AppView.Vm.Singleton
{
    class GameVm
    {
        public PileVmCollection PileCollection { get; }

        public IList<StuffVm> StuffList { get; set; } = new ObservableCollection<StuffVm>();

        private readonly IGame _model;

        private Dispatcher _dispatcher;

        public GameVm(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            _model = SingletonAccessor.GetGame();
            _model.StuffCollection.List.CollectionChanged += StuffList_CollectionChanged;
            PileCollection = new PileVmCollection(_model.PileCollection);
            _model.Initialize();
        }

        public void Start()
        {
            var timer = new Timer(15);
            timer.Elapsed += FrameUpdateTimer_Update;
            timer.Start();
        }

        /// <summary>指定座標をタップした場合の動作</summary>
        public void TapAction(Point point)
        {
            _model.TapAction(point);
        }


        /// <summary>
        /// 1フレームごとの初期
        /// </summary>
        protected void FrameUpdateTimer_Update(object sender, ElapsedEventArgs e)
        {
            try
            {
                _dispatcher.Invoke(_model.FrameUpdate);
            }
            catch (TaskCanceledException)
            {
                // nop
            }
        }

        private void StuffList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var creator = new StuffVmCreator();
                    foreach (var item in e.NewItems.OfType<IStuff>())
                    {
                        StuffList.Add(creator.Create(item));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems.OfType<IStuff>())
                    {
                        var removeItem = StuffList.FirstOrDefault(i => i.Id == item.Id);
                        StuffList.Remove(removeItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    StuffList.Clear();
                    break;
            }
        }
    }
}
