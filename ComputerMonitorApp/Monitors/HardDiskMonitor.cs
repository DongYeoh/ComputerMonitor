using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp.Monitors
{
    public class HardDiskMonitor : Monitors.Monitor
    {
        private PerformanceCounter performanceCounter;
        public HardDiskMonitor() : base(EMonitorType.HardDisk)
        {
            performanceCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            EnableTimer(1000, Timer_Elapsed);
        }
        private void Timer_Elapsed()
        {
            var value = performanceCounter.NextValue();
            OnMonitorChanged(new HardDiskMonitorEventArgs() { TotalDiskTime = value });
        }
    }
    public class HardDiskMonitorEventArgs : MonitorEventArgs
    {
        public float TotalDiskTime { get; set; }
    }
}
