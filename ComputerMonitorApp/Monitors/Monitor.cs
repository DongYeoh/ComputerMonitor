using ComputerMonitorApp.MonitorControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp.Monitors;
public abstract class Monitor : IMonitor,IDisposable
{
    public EMonitorType MonitorType { get; private set; }

    public Monitor(EMonitorType monitorType)
    {
        this.MonitorType = monitorType;
    }
    public abstract void Dispose();

    public delegate void MonitorChangedHandler(Monitor sender, MonitorEventArgs e);

    public event MonitorChangedHandler? MonitorChanged;

    public void OnMonitorChanged(MonitorEventArgs e)
    {
        MonitorChanged?.Invoke(this, e);
    }
}
