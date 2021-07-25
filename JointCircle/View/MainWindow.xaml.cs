using AppView.Vm;
using System.Windows;
using System.Windows.Input;

namespace AppView
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameVm _gameVm;

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
