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

namespace ComputerMonitorApp.MonitorControls;
public partial class CPUMonitorControl : MonitorControl
{
    public CPUMonitorControl()
    {
        this.Monitor = new CPUMonitor();
        InitializeComponent();
        
    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.Monitor!.MonitorChanged += Monitor_MonitorChanged;
    }
    private void Monitor_MonitorChanged(Monitors.Monitor sender, MonitorEventArgs e)
    {
        var e1 = e as CPUMonitorEventArgs;
        if (e1 == null) return;
        SafeInvoke(() => {
            this.labelLoad.Text = ((int)e1.Usage) + "%";
            this.chartControl.AddValue(0.5); //new Random().NextDouble()*100
        });
    }

    
    
}
