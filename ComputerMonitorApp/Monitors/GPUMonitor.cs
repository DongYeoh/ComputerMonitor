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
                EnableTimer(1000, Timer_Elapsed);
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
        private void Timer_Elapsed()
        {
            this.gpu?.Update();
            var eventArgs = new GPUMonitorEventArgs();
            eventArgs.Load = this.loadSensor?.Value ?? 0;
            eventArgs.Temperature = this.temperatorSensor?.Value ?? 0;
            eventArgs.RamLoad = this.ramLoadSensor?.Value ?? 0;
            eventArgs.Fan = this.fanSensor?.Value ?? 0;
            OnMonitorChanged(eventArgs);
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