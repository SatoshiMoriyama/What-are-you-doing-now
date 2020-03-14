using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WAYDN
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel vm = new MainViewModel();
        private HotKeyHelper hotKey;
        private Timer timer;

        public MainWindow()
        {
            InitializeComponent();

            //Ctrl+Shift+Xで画面を表示。
            hotKey = new HotKeyHelper(this);
            hotKey.Register(ModifierKeys.Control | ModifierKeys.Shift,Key.X,
                      (a, b) => { Show(); });

            //表示された時にフォーカスを合わせる。
            Activated += OnActivate;
            Deactivated += OnDeactivate;

            //ViewModelの設定
            DataContext = vm;

            if (Properties.Settings.Default.Timer != 0)
            {
                //一定間隔ごとに画面を表示し、今なにしてるかを聞く。
                timer = new Timer(Properties.Settings.Default.Timer * 1000);
                timer.Elapsed += (a, b) => {
                    //別スレッドなので。
                    Dispatcher.Invoke(new Action(() =>
                    {
                        Show();
                    }));
                };

                timer.Start();
            }
        }

        ~MainWindow()
        {
            hotKey.Dispose();
        }

        private void OnActivate(object sender, EventArgs e)
        {
            InputText.Focus();
            if (Properties.Settings.Default.Timer != 0)
                timer.Stop();
        }

        private void OnDeactivate(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Timer != 0)
                timer.Start();
        }

        private void InputText_KeyUp(object sender, KeyEventArgs e)
        {
            // Ctrl+Enterが押されたとき。
            if (e.Key == Key.Enter 
                && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (vm.InputText != "") vm.CaptureCtrlEnterAsync();
                this.Hide();
            }
        }
    }
}
