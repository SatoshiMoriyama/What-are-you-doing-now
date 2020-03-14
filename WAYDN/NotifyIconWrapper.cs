using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WAYDN
{
    public partial class NotifyIconWrapper : Component
    {
        private MainWindow main;

        public NotifyIconWrapper()
        {
            InitializeComponent();

            this.notifyIcon1.DoubleClick += this.OnOpen;
            this.toolStripMenuItem_Open.Click += this.OnOpen;
            this.toolStripMenuItem_Exit.Click += this.OnExit;

            main = new MainWindow();
        }

        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        private void OnOpen(object sender, EventArgs e)
        {
            main.Show();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
