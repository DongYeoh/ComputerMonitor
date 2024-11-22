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
    public partial class GPUMonitorControl : MonitorControl
    {
        public GPUMonitorControl()
        {
            InitializeComponent();
            ApplyTheme(new BlackTheme());
        }
        public override void ApplyTheme(MonitorControlTheme theme)
        {
            base.ApplyTheme(theme);
            this.nameLabel1.ForeColor = theme.NameColor;
            this.labelLoad.ForeColor = theme.ValueColor;
            this.labelOther.ForeColor = theme.ValueColor;
            this.chartControlLoad.ForeColor = theme.ChartLineColor;
        }
        protected override void OnMonitorChanged(MonitorEventArgs e)
        {
            base.OnMonitorChanged(e);
            var data = e as GPUMonitorEventArgs;
            if (data == null) return;
            SafeInvoke(() =>
            {
                this.chartControlLoad.AddValue(data.Load);
                this.labelLoad.Text = (int)data.Load + "%";
                this.labelOther.Text = $"{(int)data.RamLoad}% {(int)data.Temperature}° {(int)data.Fan}";
                //this.labelTemperature.Text = (int)data.Temperature + "°";
            });
            
        }
    }
}
