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
    public partial class CPUMonitorControl : MonitorControl
    {
        public CPUMonitorControl()
        {
            InitializeComponent();
            ApplyTheme(new BlackTheme());
        }

        public override void ApplyTheme(MonitorControlTheme theme)
        {
            base.ApplyTheme(theme);
            this.nameLabel1.ForeColor = theme.NameColor;
            this.labelLoad.ForeColor = theme.ValueColor;
            this.chartControl.ForeColor = theme.ChartLineColor;
            this.chartControl.BackColor = theme.BackgroundColor;
        }
        protected override void OnMonitorChanged(MonitorEventArgs e)
        {
            base.OnMonitorChanged(e);
            var e1 = e as CPUMonitorEventArgs;
            if (e1 == null) return;
            SafeInvoke(() =>
            {
                this.labelLoad.Text = ((int)e1.Load) + "%";
                this.chartControl.AddValue(e1.Load);// new Random().NextDouble() * 100);
            });
        }
    }
}
