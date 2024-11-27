using ComputerMonitorApp.MonitorControls;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ComputerMonitorApp.Monitors
{
    public abstract class Monitor : IMonitor, IDisposable 
    {
        public EMonitorType MonitorType { get; private set; }

        private Timer timer;

        public Monitor(EMonitorType monitorType)
        {
            this.MonitorType = monitorType;
        }
        private bool isRunning = false;
        private object lockOjb = new object();
        public void EnableTimer(double interval,Action timer_Elapsed)
        {
            StopTimer();
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                lock (lockOjb)
                {
                    if (isRunning) return;
                    isRunning = true;
                }
                try
                {
                    timer_Elapsed();
                    isRunning = false;
                }
                catch(Exception ex)
                {
                    Log.Error(ex,$"执行{this.MonitorType.ToString()}的timer报错，将停止运行!"+ex.Message);
                    StopTimer();
                }
            };
            timer.Start();
        }

        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }
        public virtual void Dispose() {
            StopTimer();
        }

        public delegate void MonitorChangedHandler(Monitor sender, MonitorEventArgs e);

        public event MonitorChangedHandler MonitorChanged;

        public void OnMonitorChanged(MonitorEventArgs e)
        {
            MonitorChanged?.Invoke(this, e);
        }
    }
}
