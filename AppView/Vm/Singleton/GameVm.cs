using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using AppModel;
using AppModel.IF.Singleton;

namespace AppView.Vm.Singleton
{
    class GameVm
    {
        /// <summary>杭のリスト</summary>
        public PileVmCollection PileCollection { get; }

        /// <summary>モノのリスト</summary>
        public StuffVmCollection StuffCollection { get; }

        private readonly IGame _model;

        private readonly Dispatcher _dispatcher;

        public GameVm(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            _model = SingletonAccessor.GetGame();
            PileCollection = new PileVmCollection(_model.PileCollection);
            StuffCollection = new StuffVmCollection(_model.StuffCollection);
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

    }
}
