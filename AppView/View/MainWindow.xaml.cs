using System.Windows.Input;
using AppView.Vm.Singleton;

namespace AppView.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        private readonly GameVm _gameVm;

        public MainWindow()
        {
            _gameVm = new GameVm(Dispatcher);
            _gameVm.Start();
            InitializeComponent();
            DataContext = _gameVm;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _gameVm.TapAction(e.GetPosition(this));
        }
    }
}
