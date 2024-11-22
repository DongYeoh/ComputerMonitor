using OpenHardwareMonitor.Hardware;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMonitorApp.Monitors
{
    public class GPUMonitor : Monitors.Monitor
    {
        private System.Timers.Timer timer;
        private IHardware gpu;
        private ISensor loadSensor;
        private ISensor temperatorSensor;
        private ISensor ramLoadSensor;
        private ISensor fanSensor;
        public GPUMonitor(IHardware gpu) : base(EMonitorType.GPU)
        {
            if (gpu != null)
            {
                this.gpu = gpu;
                InitSensors();
                //获取传感器
                timer = new System.Timers.Timer(1000);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }
        }
        private void InitSensors()
        {
            var gpu = this.gpu;
            foreach (var sensor in gpu.Sensors)
            {
                //Log.Debug($"{sensor.Name}:{sensor.SensorType}");
                if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("Memory"))
                {
                    this.ramLoadSensor = sensor;
                }
                else if (sensor.SensorType == SensorType.Temperature)
                {
                    this.temperatorSensor = sensor;
                }
                else if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("GPU Core"))
                {
                    this.loadSensor = sensor;
                }
                else if(sensor.SensorType == SensorType.Fan)
                {
                    this.fanSensor = sensor;
                }

            }
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.gpu?.Update();
            var eventArgs = new GPUMonitorEventArgs();
            eventArgs.Load = this.loadSensor?.Value ?? 0;
            eventArgs.Temperature = this.temperatorSensor?.Value ?? 0;
            eventArgs.RamLoad = this.ramLoadSensor?.Value ?? 0;
            eventArgs.Fan = this.fanSensor?.Value ?? 0;
            OnMonitorChanged(eventArgs);
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
    internal class GPUMonitorEventArgs : MonitorEventArgs
    {
        public float Load { get; set; }
        public float Temperature { get; set; }

        public float RamLoad { get; set; }

        public float Fan { get; set; }
    }
}