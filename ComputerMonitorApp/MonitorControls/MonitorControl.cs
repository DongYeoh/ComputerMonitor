using ComputerMonitorApp.Monitors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerMonitorApp.MonitorControls
{
    public partial class MonitorControl : UserControl, IMonitorControl 
    {
        public EMonitorType MonitorType { get; set; }
        public Monitors.Monitor Monitor { get; private set; }
        public MonitorControl()
        {
            InitializeComponent();
        }
        public void BindMonitor(Monitors.Monitor monitor)
        {
            if (this.Monitor != null)
            {
                this.Monitor.MonitorChanged -= Monitor_MonitorChanged;
            }
            this.Monitor = monitor;
            this.Monitor.MonitorChanged += Monitor_MonitorChanged;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        
        private void Monitor_MonitorChanged(Monitors.Monitor sender, MonitorEventArgs e)
        {
            OnMonitorChanged(e);
        }
        protected virtual void OnMonitorChanged(MonitorEventArgs e)
        {

        }
        public virtual void ApplyTheme(MonitorControlTheme theme)
        {
            this.BackColor = theme.BackgroundColor;

        }
        protected void SafeInvoke(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
