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
    public partial class NetworkMonitorControl : MonitorControl
    {
        public NetworkMonitorControl()
        {
            InitializeComponent();
            ApplyTheme(new BlackTheme());
        }
        public override void ApplyTheme(MonitorControlTheme theme)
        {
            base.ApplyTheme(theme);
            this.nameLabel1.ForeColor = theme.NameColor;
            this.valueLabelDowanload.ForeColor = theme.ValueColor;
            this.valueLabelUpload.ForeColor = theme.ValueColor;
        }
        protected override void OnMonitorChanged(MonitorEventArgs e)
        {
            base.OnMonitorChanged(e);
            if (e is NetworkMonitorEventArgs args)
            {

                this.SafeInvoke(() =>
                {
                    this.valueLabelDowanload.Text = "↓"+ FormatSpeed(args.ReceiveSpeed);
                    this.valueLabelUpload.Text = "↑" + FormatSpeed(args.SendSpeed);
                });
            }
        }
        private String FormatSpeed(float speed)
        {
            if (speed < 1024)
            {
                return (int)speed + "B/s";
            }
            else if (speed < 1024 * 1024)
            {
                return (int)(speed/1024) + "KB/s";
            }
            else
            {
                return (int)(speed / (1024*1024)) + "MB/s";
            }
        }

    }
}
