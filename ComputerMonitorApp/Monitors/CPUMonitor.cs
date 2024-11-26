using ComputerMonitorApp.MonitorControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp.Monitors
{
    internal class CPUMonitor : Monitor
    {
        private PerformanceCounter cpuCounter;
        public CPUMonitor() : base(EMonitorType.CPU)
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            EnableTimer(1000, Timer_Elapsed);
        }
        private void Timer_Elapsed()
        {
            var usage = cpuCounter.NextValue();
            OnMonitorChanged(new CPUMonitorEventArgs(usage));
        }
    }
    internal class CPUMonitorEventArgs : MonitorEventArgs
    {
        public float Load { get; private set; }
        public CPUMonitorEventArgs(float load)
        {
            this.Load = load;
        }
    }
}
