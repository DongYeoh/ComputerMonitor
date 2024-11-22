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
        private System.Timers.Timer timer;
        public CPUMonitor() : base(EMonitorType.CPU)
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var usage = cpuCounter.NextValue();
            OnMonitorChanged(new CPUMonitorEventArgs(usage));
        }
        public override void Dispose()
        {
            base.Dispose();
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
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
